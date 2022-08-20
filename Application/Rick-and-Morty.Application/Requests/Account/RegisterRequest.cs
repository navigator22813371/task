using System;
using System.Collections.Generic;
using System.Text;

namespace Rick_and_Morty.Application.Requests.Account
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
