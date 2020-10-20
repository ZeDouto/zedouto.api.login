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
        private const int TOKEN_EXPIRATION_HOUR = 2;
        private readonly JwtConfigurationSettings _jwtSettings;

        public UserJwtService(JwtConfigurationSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        
        public async Task<UserToken> GetToken(User model)
        {
            var claims = new[]
            {
                new Claim(nameof(model.Login), model.Login),
                new Claim(nameof(model.Password), model.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var expiration = DateTime.UtcNow.AddHours(_jwtSettings.HourExpiration);
            
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: credentials);
            
            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}