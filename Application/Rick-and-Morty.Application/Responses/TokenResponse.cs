using System;

namespace Rick_and_Morty.Application.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
