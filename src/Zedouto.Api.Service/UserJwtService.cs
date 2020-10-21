using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Zedouto.Api.Model.Configurations;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Model.Models;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Service
{
    public class UserJwtService : IJwtService<User>
    {
        private readonly JwtConfigurationSettings _jwtSettings;

        public UserJwtService(JwtConfigurationSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        
        public async Task<UserToken> GetToken(User model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var expiration = DateTime.UtcNow.AddHours(_jwtSettings.HourExpiration);
            
            var token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: credentials
            );
            
            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}