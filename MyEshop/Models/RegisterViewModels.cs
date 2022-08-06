using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class RegisterViewModels
    {
        
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(300)]
        [EmailAddress]
        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [DisplayName("پسوورد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("تکرار پسوورد")]
        public string RePassword { get; set; }

    }
}
