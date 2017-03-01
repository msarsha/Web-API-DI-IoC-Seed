using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seed.Web.AuthServer.BL
{
    public class AudienceModel
    {
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}