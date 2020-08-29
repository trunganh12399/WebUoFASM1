using System.Collections.Generic;

namespace WebUoFASM1.Models
{
    public class GroupedUserViewModel
    {
        public List<UserViewModel> Staffs { get; set; }
        public List<UserViewModel> Admins { get; set; }
        public List<UserViewModel> Trainers { get; set; }
        public List<UserViewModel> Trainees { get; set; }
    }

    public class UserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}