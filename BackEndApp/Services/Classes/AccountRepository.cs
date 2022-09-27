using BackEndApp.Models;
using BackEndApp.Services.Dtos;
using BackEndApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEndApp.Services.Classes
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly IPasswordHasher<User> passwordHasher;
        public AccountRepository(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.passwordHasher = passwordHasher;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await context.Users
                .Where(x => x.UserName == dto.UserName || x.Email == dto.UserName)
                .FirstOrDefaultAsync();

            var loginResult = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (loginResult == SignInResult.Success)
            {
                return GenerateAccessToken(user, new List<string>(), DateTime.Now.AddDays(10));
            }

            return "";
        }

        public async Task<int> Signup(SignupDto dto)
        {
            User user = new()
            {
                UserName = dto.UserName,
                Email = dto.UserName + "@random.com",
                FullName = dto.FullName
            };
            var identityResult = await userManager.CreateAsync(user, dto.Password);

            if (!identityResult.Succeeded)
                return -1;

            await context.SaveChangesAsync();

            return user.Id;
        }

        public string GenerateAccessToken(User user, IList<string> roles, DateTime expierDate)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                  configuration["Jwt:Issuer"],
                  claims,
                  expires: expierDate,
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
