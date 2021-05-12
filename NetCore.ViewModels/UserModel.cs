using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public bool Status { get; set; }
    }
}
