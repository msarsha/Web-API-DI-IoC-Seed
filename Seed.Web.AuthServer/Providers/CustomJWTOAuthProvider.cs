using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Seed.Web.AuthServer.BL;
using Seed.Web.AuthServer.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Seed.Web.AuthServer.Providers
{
    public class CustomJWTOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            AudienceManager manager = new AudienceManager();
            AudienceModel audience = manager.Find(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                // validate user credentials
                if (context.UserName == context.Password)
                {
                    context.SetError("invalid_grant", "Invalid username or password");
                    return Task.FromResult<object>(null);
                }

                var identity = new ClaimsIdentity("JWT");

                //Call Db and check if user exists in db
                bool authSuccess = true;

                if (authSuccess)
                {
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "SeedUser"));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, "Seed"));
                    identity.AddClaim(new Claim(ClaimTypes.Surname, "Seed"));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                             CustomJwtFormat.PublicKeyPropertyKey, (context.ClientId == null) ? string.Empty : context.ClientId
                        }
                    });

                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                    return Task.FromResult<object>(null);
                }
            }
            catch (Exception ex)
            {
                context.SetError("unauthorized", "Error Ocurred");
                return Task.FromResult<object>(null);
            }

            context.SetError("unauthorized", "Not authorized");
            return Task.FromResult<object>(null);
        }
    }
}