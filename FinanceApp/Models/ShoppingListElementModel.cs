using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Models
{
    public class ShoppingListElementModel : Controller
    {
        public string Id { get; set; }

        public string ShoppingListId { get; set; }
    
        public string ProductsName { get; set; }

        public int ProductsCount { get; set; }

    }
}
