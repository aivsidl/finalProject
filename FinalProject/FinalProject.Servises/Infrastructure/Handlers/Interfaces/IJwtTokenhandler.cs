using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Infrastructure.Handlers.Interfaces
{
    public interface IJwtTokenhandler
    {
        public Task<string> CreateJWTTokenAsync(string username);

    }


}
