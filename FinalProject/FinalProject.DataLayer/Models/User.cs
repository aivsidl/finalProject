using System.ComponentModel.DataAnnotations;

namespace FinalProject.DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }

        public string Salt { get; set; }
       
        public Role Role { get; set; } = Role.User;

        public UserInfo UserInfo { get; set; }
    }
}
