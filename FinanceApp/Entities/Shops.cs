namespace FinanceApp.Entities
{
    public class Shops
    {

        public int  Id { get; set; }

        public string Name { get; set; }

        public List<Documents> Documents { get; set; } = new List<Documents>();
    }
}
