namespace FinanceApp.Entities
{
    public class ProductGroup
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Products> Products { get; set; }
    }
}
