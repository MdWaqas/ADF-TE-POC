using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;

namespace ADF_TE_POC.Services
{
    public interface IUserService
    {
        Task<User> Validate(string email, string password);
    }
}
