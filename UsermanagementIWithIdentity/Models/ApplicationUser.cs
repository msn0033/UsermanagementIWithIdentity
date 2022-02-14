using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UsermanagementIWithIdentity.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "profile Picture")]
        public  byte[] ? profilePicture { get; set; }

}
}
