using System.ComponentModel.DataAnnotations;

namespace UsermanagementIWithIdentity.ViewModel
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage ="يجب ادخال الاسم")]
        public string FirstName { get; set;}
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="غير متطابقة كلمة المرور")]
        public string ConfirmPassword { get; set; }
        public List<RolesViewModel> roles { get; set; }
    }
}
