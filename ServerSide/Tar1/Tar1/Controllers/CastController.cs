using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tar1.BL;


namespace Tar1.Controllers
{
    [Route("api/[controller]")]
    public class CastController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Cast> Get()
        {
            return Cast.Read();
        }

        // GET api/values/5
        [HttpGet("{MovieId}")]
        public object GetCast4Movie(int MovieId)
        {
            return Cast.GetCast4Movie(MovieId);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Cast c)
        {
            if (Cast.Insert(c)==1)
            {
                return Ok(new { message = "Cast member added successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to add cast member" });
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

