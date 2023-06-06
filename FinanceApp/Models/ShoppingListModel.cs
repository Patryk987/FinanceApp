using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Models
{
    public class ShoppingListModel : Controller
    {
        public string Id { get; set; }
        public string ListName { get; set; }
        public List<ShoppingListElementModel> ListElement { get; set; } 
    }
}
