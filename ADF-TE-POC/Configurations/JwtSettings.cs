using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ADF_TE_POC.Configurations
{
    public class JwtSettings
    {
        #region Properties

        public string Audience { get; set; }

        public double ClockSkewSeconds { get; set; }

        public Lazy<EncryptingCredentials> EncryptingCredentials => new Lazy<EncryptingCredentials>(() => new EncryptingCredentials(EncryptingSymmetricSecurityKey.Value, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256));

        public string EncryptingSecretKey { get; set; }

        public Lazy<SymmetricSecurityKey> EncryptingSymmetricSecurityKey => new Lazy<SymmetricSecurityKey>(() => GetSymmetricSecurityKey(EncryptingSecretKey));

        public Lazy<DateTime> ExpiresUtc => new Lazy<DateTime>(() => IssuedAtUtc.Value.AddMinutes(ValidForMinutes));

        public Lazy<DateTime> IssuedAtUtc => new Lazy<DateTime>(() => DateTime.UtcNow);

        public string Issuer { get; set; }

        public Lazy<DateTime> NotBeforeUtc => new Lazy<DateTime>(() => IssuedAtUtc.Value);

        public Lazy<SigningCredentials> SigningCredentials => new Lazy<SigningCredentials>(() => new SigningCredentials(SigningSymmetricSecurityKey.Value, SecurityAlgorithms.HmacSha512));

        public string SigningSecretKey { get; set; }

        public Lazy<SymmetricSecurityKey> SigningSymmetricSecurityKey => new Lazy<SymmetricSecurityKey>(() => GetSymmetricSecurityKey(SigningSecretKey));

        public double ValidForMinutes { get; set; }

        #endregion


        #region Private Methods

        private static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        #endregion
    }
}
