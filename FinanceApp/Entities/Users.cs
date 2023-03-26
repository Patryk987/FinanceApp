﻿namespace FinanceApp.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; } //adress email
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }


        //relacja 1 do wiele do Payments
        public List(Payments) Payment { get; set;} = new List<Payments>;
        // relacja 1 do wiele do Documents
        public List<Documents> Document { get; set; } = new List<Documents>();
        //relacja wiele do 1 do FamilyGroup
        public FamilyGroup FamilyGroup { get; set; }
        public int FamilyGroupId { get; set; }

    }
}
