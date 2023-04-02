namespace FinanceApp.Models
{
    public class DocumentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public int IdShop { get; set; }

        public string Desc { get; set; } = null!;

        public List<DocPosDto> DocumentPo { get; set; }
    }
}
