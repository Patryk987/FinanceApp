namespace FinanceApp.Entities
{
    public partial class Users
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public int IdGroup { get; set; }

        public string Password { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public virtual ICollection<Document> Documents { get; } = new List<Document>();

        public virtual ICollection<Payment> Payments { get; } = new List<Payment>();
    }
}
