using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.Services.Impl
{
    public class UserServices : IUserService
    {
        private readonly InventoryContext _inventory;

        public UserServices(InventoryContext inventory)
        {
            _inventory = inventory;
        }

        public async Task<User> Validate(string email, string password)
        {
            var user = await _inventory.Users.FirstOrDefaultAsync(t => t.Email == email && t.Password == password);
            return user;
        }
    }
}
