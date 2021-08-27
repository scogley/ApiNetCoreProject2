using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNetCoreProject2.Models
{
    public class UserModel
    {
        // User Id(service or database auto-generated guid)
        public Guid UserId { get; set; }
        // User Email(required)
        public string Email { get; set; }
        // User Password(required) store it in plain text or better yet hashed
        public string Password { get; set; }
        // Created Date(service generated)
        public DateTime CreatedDate { get; set; }
    }
}
