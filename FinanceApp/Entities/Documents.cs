namespace FinanceApp.Entities
{
    public class Documents
    {
        public int Id { get; set; }

        public string Name { get; set; }



        public Shops shops { get; set; }
    }
}
