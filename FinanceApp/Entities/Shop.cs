namespace FinanceApp.Entities
{
    public partial class Shop
    {
        public int IdShop { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; } = new List<Document>();
    }
}
