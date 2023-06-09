namespace FinanceApp.Models
{
    public class CyclicalExpensesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StartData { get; set; }

        public string Periods { get; set; }

        public decimal Amount { get; set; }

        public decimal AmountPln { get; set; }

        public string Currency { get; set; }

        public string? Groups { get; set; }

        public int UserId { get; set; }


    }
}
