using Microsoft.Owin.Security.DataHandler.Encoder;
using Seed.Web.AuthServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Seed.Web.AuthServer.BL
{
    public class AudienceManager
    {
        public AudienceManager()
        {
        }

        public static AudienceModel Create(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            AudienceModel newAudience = new AudienceModel { PublicKey = clientId, PrivateKey = base64Secret, Name = name };
            AudienceFakeDB.Add(new AudienceModel() { Name = newAudience.Name, PrivateKey = newAudience.PrivateKey, PublicKey = newAudience.PublicKey });
            //_repo.Add(new Audience() { Name = newAudience.Name, PrivateKey = newAudience.PrivateKey, PublicKey = newAudience.PublicKey });

            return newAudience;
        }

        public AudienceModel Find(string publicKey)
        {
            AudienceModel res = AudienceFakeDB.Find(publicKey);
            //Audience res = _repo.Find(publicKey);

            if (res == null)
                throw new NullReferenceException("no audience");

            return res;
        }
    }
}