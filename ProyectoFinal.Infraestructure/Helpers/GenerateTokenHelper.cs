﻿using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Core.DTOs.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActiveDirectoryBack.Infrastructure.Helpers
{
    public static class GenerateTokenHelper
    {        
        public static string GenerateToken(UsersDTO User, string Signature, int Hours)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserName", User.Names),
                        new Claim(ClaimTypes.Email, User.Email),
                        new Claim("UserId", User.IdUser.ToString()),
                        new Claim("IdRol", User.idRol.ToString()),
                    }
                    ),
                Expires = DateTime.UtcNow.AddHours(Hours),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
