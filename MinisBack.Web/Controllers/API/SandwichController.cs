using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MinisBack.Data;
using System.Data.Entity;

namespace MinisBack.Web.Controllers.API
{
    [RoutePrefix("api/sandwich")]
    public class SandwichController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetAll()
        {
            using (var repository = new MinisBackContext())
            {
                var retVal = repository.Sandwichs.Include(x => x.Ingredients).ToList();
                return Ok(retVal);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Store()
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update()
        {
            return Ok();
        }
    }
}
