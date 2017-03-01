using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Seed.Web.AuthServer.BL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Tokens;

namespace Seed.Web.AuthServer.Formatters
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public static string PublicKeyPropertyKey = "publicKey";

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string publicKey = data.Properties.Dictionary.ContainsKey(PublicKeyPropertyKey) ? data.Properties.Dictionary[PublicKeyPropertyKey] : null;

            if (string.IsNullOrWhiteSpace(publicKey)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            AudienceManager manager = new AudienceManager();
            AudienceModel audience = manager.Find(publicKey);

            string symmetricKeyAsBase64 = audience.PrivateKey;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, publicKey, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}