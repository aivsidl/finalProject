using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FinalProject.DataLayer.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(11)]
        public string PersonalCode { get; set; }
        [Required, MaxLength(16)]
        public string Phone { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }

        //public byte[] Photo { get; set; }
        public User User { get; set; }

        public Adress Adress { get; set; }













    }
}
