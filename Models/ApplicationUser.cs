using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class ApplicationUser: IdentityUser
    {
        public override string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public string UserRoleId { get; set; }
        public IdentityRole UserRole { get; set; }

        public ICollection<UsersTask> UserTasks { get; set; } = new HashSet<UsersTask>();
      

    }

}
