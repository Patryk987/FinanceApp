namespace FinanceApp.Entities
{
    public class ProductGroup
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Products Product { get; set; }

        public int ProductId { get; set; }
    }
}
