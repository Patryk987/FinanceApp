using System;
using System.Collections.Generic;

namespace FinanceApp.Entities;

public partial class DocumentPo
{
    public int Id { get; set; }

    public int IdDoc { get; set; }

    public int IdProd { get; set; }

    public decimal Price { get; set; }

    public DateTime Date { get; set; }

    public virtual Document IdDocNavigation { get; set; } = null!;

    public virtual Product IdProdNavigation { get; set; } = null!;
}