using FinalProject.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Models
{
    public class UserInfoDto
    {
        public int Id { get; set; }
      
        public string FirstName { get; set; }
     
        public string LastName { get; set; }
      
        public string PersonalCode { get; set; }
       
        public string Phone { get; set; }
      
        public string Email { get; set; }


        //public byte[] Photo { get; set; }       

        public UserAdressDto UserAdressDto { get; set; }

    }
}
