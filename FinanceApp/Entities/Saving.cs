namespace FinanceApp.Entities
{
    public partial class Saving
    {
        public int Id { get; set; }

        public int Idpaymants { get; set; }

        public string Name { get; set; } = null!;

        public virtual Payment IdpaymantsNavigation { get; set; } = null!;
    }
}
