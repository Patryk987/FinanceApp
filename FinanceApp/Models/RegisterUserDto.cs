using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class RegisterUserDto
    {

  
        public string Login { get; set; }
    
        public string Name { get; set; }
   
        public string Surname { get; set; }
      
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
