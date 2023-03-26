namespace FinanceApp.Entities
{
    public class Payments
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public Savings Savings { get; set; }
    }
}
