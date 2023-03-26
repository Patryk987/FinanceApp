namespace FinanceApp.Entities
{
    public class DocumentPos
    {
        public int Id { get; set; }
        
        decimal  Price { get; set; }

        public DateTime DataPos { get; set; }

        //Relacja Wiele do 1 z Documents
        public Documents Document { get; set; }
        public int DocumentId { get; set; }

        //Relacja Wiele do 1 z Products
        public Products Products { get; set; }
        public int ProductId { get; set; }

        
    }
}
