﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Models
{
    public class RegisterUser : LoginDto
    {
      public string CheckPassword { get; set; }

    }
}
