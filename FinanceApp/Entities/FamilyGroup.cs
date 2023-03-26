namespace FinanceApp.Entities
{
    public class FamilyGroup
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Users user { get; set; }

        public int UserId { get; set; }
    }
}
