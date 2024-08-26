using FinalProject.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddAsync(UserDto userDto);
    }
}
