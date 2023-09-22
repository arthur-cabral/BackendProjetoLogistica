using Application.DTO.Security;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<CreateToken> Login(UserDTO userDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password,
            isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return GenerateToken(userDTO);
            }
            else
            {
                throw new AuthenticationException("Username or password are incorrect");
            }
        }

        public async Task<CreateToken> Register(UserDTO userDTO)
        {
            IdentityUser createUser = new()
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(createUser, userDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(createUser, false);
                return GenerateToken(userDTO);
            }
            else
            {
                throw new Exception(result.Errors.ToString());
            }

        }

        private CreateToken GenerateToken(UserDTO userDTO)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userDTO.Email),
                new Claim("meuPet", "dobby"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            string expirationConfig = _configuration["TokenConfiguration:ExpireHours"];
            DateTime expiration = DateTime.UtcNow.AddHours(double.Parse(expirationConfig));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new CreateToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT ok"
            };
        }

    }
}
