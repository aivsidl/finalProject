﻿namespace FinalProject.API.Models
{
    public class UserAdressRequest
    {
      
        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public int? ApartamentNumber { get; set; }
    }
}
