namespace Rick_and_Morty.Application.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public virtual string FullName => this.LastName + ' ' + this.FirstName + ' ' + this.Patronymic;
        public string Avatar { get; set; }
    }
}
