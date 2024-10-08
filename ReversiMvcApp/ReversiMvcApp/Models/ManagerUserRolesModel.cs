using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ReversiMvcApp.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}