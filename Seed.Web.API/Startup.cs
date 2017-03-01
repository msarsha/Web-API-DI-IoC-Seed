using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(Seed.Web.API.Startup))]

namespace Seed.Web.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string issuer = "seed.authserver";
            string audience = "60f783b666a94ec79a70b268e9a256df"; // public key
            byte[] secret = TextEncodings.Base64Url.Decode("jKQwDwKcutNWEndCiS1hM6GF7cdkr2T-exG_FuY41yg"); // private key

            var options = new JwtBearerAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                },
                AllowedAudiences = new List<string>() { audience },
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    // override to add custom claims as needed by your application
                    OnValidateIdentity = ctx => { return Task.FromResult<object>(null); }
                }
            };
            app.UseJwtBearerAuthentication(options);
        }
    }
}
