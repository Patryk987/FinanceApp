namespace FinanceApp.Models
{
    public class ProductPriceModel
    {
        public string Barcode { get; set; }
        public float Price { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
