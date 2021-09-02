using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiNetCoreProject2.Models
{
    public class UserModel
    {
        // User Id(service or database auto-generated guid)
        public Guid UserId { get; set; }
        // User Email(required)
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email property is required")]
        public string Email { get; set; }
        // User Password(required) store it in plain text or better yet hashed
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password property is required")]
        public string Password { get; set; }
        // Created Date(service generated)
        public DateTime CreatedDate { get; set; }
    }
}
