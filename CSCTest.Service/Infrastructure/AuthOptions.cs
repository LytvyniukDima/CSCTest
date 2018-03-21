using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CSCTest.Service.Infrastructure
{
    public class AuthOptions
    {
        public string Issuer { get; set; } 
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}