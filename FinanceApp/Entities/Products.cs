namespace FinanceApp.Entities
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BarCode { get; set; }

        //Relacja 1 do wielu z DocumentPos
        public List<DocumentPos> DocumentPos { get; set; }
        public int DocumentPosId { get; set; }

        //Relacja wiele do 1 z ProductGroup
        public virtual ProductGroup ProductGroup { get; set; }
        public int ProductGroupId { get; set;}

    }
}
