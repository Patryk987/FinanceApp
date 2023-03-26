namespace FinanceApp.Entities
{
    public class FamilyGroup
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Users user { get; set; }


        //Relacja 1 do wielu z User
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
