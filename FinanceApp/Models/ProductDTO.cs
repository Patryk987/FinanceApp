namespace FinanceApp.Models
{
    public class ProductDTO
    {
        public string Name { get; set; } = null!;

        public string Barcode { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public List<ProductPriceModel>? ProductPrice { get; set; }
    }
}
