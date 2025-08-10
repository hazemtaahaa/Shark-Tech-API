using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shark_Tech.DAL.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;

            public AuthRepository(UserManager<AppUser> userManager)
            {
                        _userManager = userManager;
            }

        // Implement methods for authentication here, e.g., Register, Login, etc.
        public async Task<string> RegisterAsync(string username , string email , string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return "Email already Exist"; // Email already exists
            }
            var user = new AppUser
            {
                UserName = username,
                Email = email,
                DisplayName = username // Assuming DisplayName is the same as username
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return "User registered successfully"; // Registration successful
            }
            else
            {
                return string.Join(", ", result.Errors.Select(e => e.Description)); // Return error messages
            }
        }
    }
}
