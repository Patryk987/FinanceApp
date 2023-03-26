namespace FinanceApp.Entities
{
    public class Payments
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AmountPLN { get; set; }
        public string Category { get; set; }
        public decimal AmountWal { get; set; }
        public string Currency  { get; set; }

        //Relacja 1 do 1 z Savings
        public Savings Savings { get; set; }
        //Relacja wiele do 1 z Users
        public Users Users { get; set; }
        public int UserId { get; set; }
    }
}
