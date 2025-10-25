using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mini_Backed.Models;
using Mini_Backed.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mini_Backed.Services
{
    public class UserService : IUserService
    {
        private readonly MiniOnlineLibraryContext _context;
        private readonly IConfiguration _config;

        public UserService(MiniOnlineLibraryContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Authenticate user by username and password
        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null) return null;

            // If stored password is already hashed
            if (user.PasswordHash != null && user.PasswordHash.StartsWith("$2"))
            {
                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    return null;
            }
            else
            {
                // If stored password is plain text (legacy), allow login once and hash it
                if (user.PasswordHash != password) return null;

                // Rehash password for future logins
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        // Register a new user
        public async Task<User?> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return null;

            // Hash password before saving
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Generate JWT token
        public string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Get user by ID
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Optional: Seed an admin user if none exists
        public async Task EnsureAdminExists()
        {
            if (!await _context.Users.AnyAsync(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    Name = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin"
                };
                _context.Users.Add(admin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
