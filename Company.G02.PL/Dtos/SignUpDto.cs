using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Passwrod is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Not Match")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
