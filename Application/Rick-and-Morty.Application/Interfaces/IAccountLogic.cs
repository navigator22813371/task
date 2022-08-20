using Rick_and_Morty.Application.Dtos;
using Rick_and_Morty.Application.Requests.Account;
using Rick_and_Morty.Application.Responses;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Interfaces
{
    public interface IAccountLogic
    {
        Task<Response<TokenResponse>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task<Response<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
        Task<Response<ApplicationUserDto>> GetProfileAsync(string userName);
        Task<Response<string>> UpdateProfileAsync(UpdateProfileRequest request);
    }
}
