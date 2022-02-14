using System.ComponentModel.DataAnnotations;

namespace UsermanagementIWithIdentity.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
