using FinalProject.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Models
{

    public class UserDto
    {
       
        public int Id { get; set; }
        
        public string UserName { get; set; }
      
        public Role Role { get; set; } = Role.User;

        public UserInfoDto UserInfoDto { get; set; }

    }
}
