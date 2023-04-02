namespace FinanceApp.Models
{
    public class DocPosDto
    {
        public int Id { get; set; }

        public int IdDoc { get; set; }

        public int IdProd { get; set; }

        public decimal Price { get; set; }

        public List<ProductDTO> products { get; set; }
    }
}
