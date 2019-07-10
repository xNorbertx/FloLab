using FloLab.Core.DTOs;
using FloLab.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FloLab.Core.Interfaces
{
    public interface IAuthService
    { 
        Task<string> Authenticate(User user);

        Task<User> FindUserByEmail(string Email);

        Task<User> FindUserById(string Id);

        Task<IdentityResult> CreateUser(User user);

        Task<IdentityResult> AddToRole(User user, string role);

        Task<IdentityResult> ResetPassword(User user, string token, string password);

        Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword);

        Task<SignInResult> Login(LoginDTO model);

        Task<IdentityResult> Persist(User user);
    }
}
