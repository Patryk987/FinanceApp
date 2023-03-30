namespace FinanceApp.Entities
{
    public partial class ProductGroup
    {
        public int IdGroup { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
