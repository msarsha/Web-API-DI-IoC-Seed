using Seed.Infrastructure.Models;
using Seed.Interfaces.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Seed.Web.API.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class SeedController : ApiController
    {
        private ISeedManager manager;

        public SeedController(ISeedManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(manager.FindAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(manager.FindById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(SeedDto dto)
        {
            manager.CreateSeed(dto);
            return Ok();
        }
    }
}
