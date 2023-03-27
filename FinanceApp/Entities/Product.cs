namespace FinanceApp.Entities
{
    public partial class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Barcode { get; set; } = null!;

        public int? IdGroup { get; set; }

        public virtual ICollection<DocumentPo> DocumentPos { get; } = new List<DocumentPo>();

        public virtual ProductGroup? IdGroupNavigation { get; set; }
    }
}
