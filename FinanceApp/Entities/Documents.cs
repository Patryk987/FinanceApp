namespace FinanceApp.Entities
{
    public class Documents
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public decimal Amount { get; set; }

        public string Desc { get; set; }

        public DateTime DataDokumentu { get; set; }


        //Relacja Wiele do 1 z Users
        public Users Users { get; set; }
        public int UserId { get; set; }

        //Relacja 1 do wielu z DocumentPos
        public List<DocumentPos> DocPos { get; set; } = new List<DocumentPos>();

        //Relacja Wiele do 1 z Shops
        public Shops Shops { get; set; }
        public int ShopId { get; set;}



        
    }
}
