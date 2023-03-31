using System;

namespace FinanceApp.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal AmountPln { get; set; }

        public short TypeOfPayments { get; set; }

        public int UserId { get; set; }

        public decimal AmountWal { get; set; }

        public string Waluta { get; set; } = null!;
        public virtual ICollection<Saving> Savings { get; } = new List<Saving>();

        public virtual Users User { get; set; } = null!;
    }
}
