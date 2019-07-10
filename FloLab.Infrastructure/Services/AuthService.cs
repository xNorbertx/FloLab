using FloLab.Core.DTOs;
using FloLab.Core.Entities;
using FloLab.Core.Interfaces;
using System.Security.Claims;
using FloLab.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FloLab.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            await GetRoleAsync(claims, user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task GetRoleAsync(List<Claim> claims, User user)
        {
            claims.AddRange(await _userManager.GetClaimsAsync(user));

            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String);
                    claims.Add(roleClaim);

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            };
        }

        public async Task<User> FindUserByEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email);
        }

        public async Task<User> FindUserById(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<IdentityResult> CreateUser(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> AddToRole(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ResetPassword(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync($"U{model.email}", model.password, false, false);
        }

        public async Task<IdentityResult> Persist(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
