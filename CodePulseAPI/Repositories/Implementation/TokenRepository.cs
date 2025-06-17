﻿using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulseAPI.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //tao yeu cau
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Email, user.Email)
           };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //tham so bao mat Jwt token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"],claims: claims,expires: DateTime.Now.AddMinutes(15),signingCredentials: credential );
            // tra ve token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
