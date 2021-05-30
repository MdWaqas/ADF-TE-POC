using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.Services.Impl
{
    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="ADF_TE_POC.Services.IUserService" />
    public class UserServices : IUserService
    {
        private readonly InventoryContext _inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServices"/> class.
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        public UserServices(InventoryContext inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// Validates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<User> Validate(string email, string password)
        {
            var user = await _inventory.Users.FirstOrDefaultAsync(t => t.Email == email && t.Password == password);
            return user;
        }
    }
}
