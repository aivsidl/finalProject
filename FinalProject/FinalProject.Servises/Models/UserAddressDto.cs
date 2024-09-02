using FinalProject.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Models
{
    public class UserAddressDto
    {
        public int Id { get; set; }      

        public string City { get; set; }       

        public string Street { get; set; }       

        public string HouseNumber { get; set; }

        public int? ApartamentNumber { get; set; }
        
    }
}
