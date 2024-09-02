using System.ComponentModel.DataAnnotations;

namespace FinalProject.DataLayer.Models
{
    public class Address
    {

        public int Id { get; set; }
        public int UserInfoId { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Street { get; set; }
        [Required, MaxLength(10)]
        public string HouseNumber { get; set; }

        public int? ApartamentNumber { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
