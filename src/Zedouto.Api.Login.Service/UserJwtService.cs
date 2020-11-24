using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Zedouto.Api.Login.Model.Constants.JsonPropertiesName;
using Microsoft.IdentityModel.Tokens;
using Zedouto.Api.Login.Model.Configurations;
using Zedouto.Api.Login.Model;
using Zedouto.Api.Login.Service.Interfaces;
using System.Collections.Generic;
using System.Text.Json;

namespace Zedouto.Api.Login.Service
{
    public class UserJwtService : IJwtService<User, UserToken>
    {
        private readonly JwtConfigurationSettings _jwtSettings;

        public UserJwtService(JwtConfigurationSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        
        public UserToken SerializeToken(User model)
        {
            if(model is null)
            {
                return null;
            }
            
            var claims = new List<Claim>
            {
                new Claim(NAME_PROPERTY_TEXT, model.Name),
                new Claim(CPF_PROPERTY_TEXT, model.Cpf),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if(!string.IsNullOrWhiteSpace(model.Login))
            {
                claims.Add(new Claim(LOGIN_PROPERTY_TEXT, model.Login));
            }

            if(model.Doctor != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(CRM_PROPERTY_TEXT, model.Doctor.Crm),
                    new Claim(SPECIALTY_PROPERTY_TEXT, model.Doctor.Specialty)
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

        public User DeserializeToken(UserToken token)
        {
            if(IsInvalidToken(token))
            {
                return default;
            }

            var tokenPayloadJson = new JwtSecurityTokenHandler().ReadJwtToken(token.Token).Payload.SerializeToJson();

            return JsonSerializer.Deserialize<User>(tokenPayloadJson);
        }

        private bool IsInvalidToken(UserToken userToken)
        {
            return userToken.Expiration > DateTime.Now || string.IsNullOrWhiteSpace(userToken.Token);
        }
    }
}