namespace FinanceApp.Models
{
    public class UsersDto
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public int IdGroup { get; set; }

        public string Password { get; set; }



    }
}
