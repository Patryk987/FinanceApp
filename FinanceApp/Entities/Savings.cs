namespace FinanceApp.Entities
{
    public class Savings
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        //Relacja 1 do 1 z Payments 
        public Payments payments { get; set; }
        public int IdPayments { get; set; }

    }
}
