using System;

namespace Zedouto.Api.Login.Model.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}