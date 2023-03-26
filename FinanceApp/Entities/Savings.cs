namespace FinanceApp.Entities
{
    public class Savings
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Payments payments { get; set; }

        public int IdPayments { get; set; }

    }
}
