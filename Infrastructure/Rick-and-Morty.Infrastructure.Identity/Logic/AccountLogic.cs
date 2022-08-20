using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Exceptions;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Requests.Account;
using Rick_and_Morty.Application.Responses;
using Rick_and_Morty.Infrastructure.Identity.Context;
using Rick_and_Morty.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rick_and_Morty.Infrastructure.Identity.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountLogic(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMapper mapper, 
            IdentityContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<TokenResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
                if (passwordCheck)
                {
                    return await GenerateJwtTokenAsync(user);
                }
                else
                {
                    throw new HttpException(HttpStatusCode.Forbidden, "Wrong login or password");
                }
            }

            throw new HttpException(HttpStatusCode.NotFound, "User not found");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            if (request.Password.Length > 244)
            {
                throw new HttpException(HttpStatusCode.Forbidden, "Maximum 244 characters");
            }

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
                CreateDate = DateTime.Now
            };

            await _userManager.CreateAsync(user, request.Password);

            return new Response<string>() { Succeeded = true, Message = "User Created Successfully" };
        }

        public async Task<Response<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetPrincipalFromToken(request.Token);
            if (validatedToken == null)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "Invalid Token" } };
            }

            //Проверка срок действия токена
            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (storedRefreshToken == null)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new Response<TokenResponse>
                { Succeeded = false, Error = new List<string> { "This refresh token does not match this JWT" } };
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "UserId").Value);

            return await GenerateJwtTokenAsync(user);
        }

        private async Task<Response<TokenResponse>> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:ExpiresMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new Response<TokenResponse>(
                new TokenResponse()
                { 
                    Token = tokenString, 
                    RefreshToken = refreshToken.Token 
                }, "JWT Token");
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
                };
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    throw new SecurityTokenException("Invalid token passed");
                }

                return principal;
            }
            catch
            {
                throw new HttpException(HttpStatusCode.Forbidden, "One or more validation failures have occurred");
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }


        public async Task<Response<ApplicationUserDto>> GetProfileAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "User not found");
            }

            var userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);

            return new Response<ApplicationUserDto>(userDto);
        }

        public async Task<Response<string>> UpdateProfileAsync(UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            user = _mapper.Map(request, user);

            await _userManager.UpdateAsync(user);

            return new Response<string>() { Succeeded = true, Message = "User updated successfully" };
        }
    }
}
