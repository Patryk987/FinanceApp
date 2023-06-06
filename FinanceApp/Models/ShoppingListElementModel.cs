using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Models
{
    public class ShoppingListElementModel : Controller
    {
        public int? Id { get; set; }

        public int ListId { get; set; }

        public string ProductName { get; set; }

        public bool? Status { get; set; }

    }
}
