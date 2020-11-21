using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Zedouto.Api.Login.Model.Configurations;
using Zedouto.Api.Login.Model;
using Zedouto.Api.Login.Service.Interfaces;
using System.Collections.Generic;

namespace Zedouto.Api.Login.Service
{
    public class UserJwtService : IJwtService<User, UserToken>
    {
        private readonly JwtConfigurationSettings _jwtSettings;

        public UserJwtService(JwtConfigurationSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        
        public UserToken GetToken(User model)
        {
            if(model is null)
            {
                return null;
            }
            
            var claims = new List<Claim>
            {
                new Claim(nameof(User.Name), model.Name),
                new Claim(nameof(User.Login), model.Login),
                new Claim(nameof(User.Cpf), model.Cpf),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if(model.Doctor != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(nameof(User.Doctor.Crm), model.Doctor.Crm),
                    new Claim(nameof(User.Doctor.Specialty), model.Doctor.Specialty)
                });
            }
            
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