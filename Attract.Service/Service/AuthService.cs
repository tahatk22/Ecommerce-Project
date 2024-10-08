﻿using Attract.Common.BaseResponse;
using Attract.Common.DTOs.User;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RoleManager<IdentityRole> roleManager;
        private User user;

        public AuthService(UserManager<User> userManager, IConfiguration configuration,
            IMapper mapper, SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
        }
        public async Task<BaseCommandResponse> CreateAdmins(RegisterDTO request)
        {
            var response = new BaseCommandResponse();

            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "Email Already Exist!";
                return response;
            }
            var user = CreateAdminUser(request);
            var adminRoleExists = await EnsureAdminRoleExists();
            var userRoleExists = await EnsureUserRoleExists();
            try
            {
                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    if(request.IsAdmin)
                    {
                        var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
                        if(!addToRoleResult.Succeeded)
                        {
                            response.Success = false;
                            response.Message = $"Failed to assign admin role to the user {user.UserName}";
                            return response;
                        }
                        response.Success = true;
                        response.Message = "Registerd Successfully!";
                        return response;
                    }
                    else
                    {
                        var addToRoleResult = await userManager.AddToRoleAsync(user, "User");
                        if (!addToRoleResult.Succeeded)
                        {
                            response.Success = false;
                            response.Message = $"Failed to assign admin role to the user {user.UserName}";
                            return response;
                        }
                        response.Success = true;
                        response.Message = "Registerd Successfully!";
                        return response;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "User registration failed. Please check the provided information.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred during user registration.";
                // Log the exception if needed...
                return response;
            }
            return response;
        }
        public async Task<BaseCommandResponse> AdminLogin(LoginUserDTO loginUserDTO)
        {
            var user = await userManager.FindByEmailAsync(loginUserDTO.Email);
            var response = new BaseCommandResponse();

            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found!";
                return response;
            }
            var userRole=await userManager.GetRolesAsync(user);
            if (userRole.Count == 0)
            {
                response.Success = false;
                response.Message = "User Not Found!";
                return response;
            }
            bool userRoleName = userRole.FirstOrDefault() == "Admin" ? true : false;
            var result = await signInManager.PasswordSignInAsync(user.UserName, loginUserDTO.Password, false, lockoutOnFailure: false);

            if (result.IsNotAllowed || !result.Succeeded)
            {
                response.Success = false;
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var responseAuth = new
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Role = userRole,
                isAdmin = userRoleName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Phone = user.PhoneNumber,
            };

            response.Success = true;
            response.Data = responseAuth;
            return response;
        }
        public async Task<BaseCommandResponse> Login(LoginUserDTO userDTO)
        {
            var user = await userManager.FindByEmailAsync(userDTO.Email);
            var response = new BaseCommandResponse();
            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found!";
                return response;
            }
            var userRole = await userManager.GetRolesAsync(user);
            if (userRole.Count != 0)
            {
                response.Success = false;
                response.Message = "User Not Found!";
                return response;
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, userDTO.Password, false, lockoutOnFailure: false);
            if (result.IsNotAllowed || !result.Succeeded)
            {
                response.Success = false;
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var responseAuth = new
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Phone = user.PhoneNumber,
            };
            response.Success = true;
            response.Data = responseAuth;
            return response;
        }
        public string GetCurrentUserId()
        {
            if (httpContextAccessor.HttpContext != null)
            {
                var userId = httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId != null)
                    return userId;
                else return null;
            }
            else
                return null;
        }
        public async Task<BaseCommandResponse> Register(UserDTO request)
        {
            var response = new BaseCommandResponse();

            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "Email Already Exist!";
                return response;
            }

            var user = CreateUser(request);

            try
            {
                var createUserResult = await userManager.CreateAsync(user, request.Password);

                if (createUserResult.Succeeded)
                {
                    var userFromDb = await userManager.FindByNameAsync(user.UserName);
                    response.Success = true;
                    response.Message = "Registered Successfully!";
                    response.Data = user.Id.ToString();
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = createUserResult.Errors.FirstOrDefault()?.Description ?? "Unknown error during registration.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred during registration.";
                return response;
            }
        }
        #region private methods
        private async Task<bool> EnsureAdminRoleExists()
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin".ToLower());

            if (!adminRoleExists)
            {
                var createRoleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                return createRoleResult.Succeeded;
            }

            return true;
        }
        private async Task<bool> EnsureUserRoleExists()
        {
            var userRoleExists = await roleManager.RoleExistsAsync("User".ToLower());

            if (!userRoleExists)
            {
                var createRoleResult = await roleManager.CreateAsync(new IdentityRole("User"));
                return createRoleResult.Succeeded;
            }

            return true;
        }
        private async Task<IdentityResult> AddUserToAdminRole(User user)
        {
            return await userManager.AddToRoleAsync(user, "Admin");
        }
        private User CreateAdminUser(RegisterDTO userDTO)
        {
            return new User
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.PhoneNumber.ToString()
            };
        }
        private User CreateUser(UserDTO userDTO)
        {
            return new User
            {
                UserName=userDTO.Email,
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PhoneNumber = userDTO.PhoneNumber.ToString(),
            };
        }
        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            string userRoles = string.Join(",", roles);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("UserID", user.Id.ToString()),
                 new Claim("RoleName", userRoles)
        }
            .Union(userClaims);
            var _jwtSettingss = configuration.GetSection("Jwt").Value;
            var _jwtSettingsss = configuration.GetSection("Jwt:Key").Value;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value)); // issue
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration.GetSection("Jwt:DurationInMinutes").Value)),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }        
        #endregion
    }
}
