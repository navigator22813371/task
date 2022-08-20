namespace Rick_and_Morty.Application.Requests.Account
{
    public class UpdateProfileRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
