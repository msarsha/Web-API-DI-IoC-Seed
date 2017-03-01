using Seed.Web.AuthServer.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seed.Web.AuthServer.Data
{
    public class AudienceFakeDB
    {
        private static Dictionary<string, AudienceModel> data { get; set; }

        static AudienceFakeDB()
        {
            data = new Dictionary<string, AudienceModel>();
            data.Add("60f783b666a94ec79a70b268e9a256df", new AudienceModel()
            {
                Name = "SeedApi",
                PublicKey = "60f783b666a94ec79a70b268e9a256df",
                PrivateKey = "jKQwDwKcutNWEndCiS1hM6GF7cdkr2T-exG_FuY41yg"
            });
        }

        public static void Add(AudienceModel aud)
        {
            data.Add(aud.PublicKey, aud);
        }

        public static AudienceModel Find(string publicKey)
        {
            return data[publicKey];
        }
    }
}