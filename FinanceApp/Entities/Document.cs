using System;
using System.Collections.Generic;

namespace FinanceApp.Entities;

public partial class Document
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public int IdShop { get; set; }

    public string Desc { get; set; } = null!;

    public DateTime DataDokumentu { get; set; }

    public virtual ICollection<DocumentPo> DocumentPos { get; } = new List<DocumentPo>();

    public virtual Shop IdShopNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}