using System;

namespace Rick_and_Morty.Application.Requests.Account
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
