using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tar1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tar1
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return Movie.Read();
        }

        [HttpGet("Rating/{rating}")]
        public IActionResult GetByRating(double rating)
        {
            try
            {
                List<Movie> ListByRatig = Movie.ReadByRating(rating);
                if (ListByRatig == null)
                {
                    return BadRequest("Please insert Valid rating");
                }
                else
                {
                    return Ok(ListByRatig);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת
            }

        }

        [HttpGet("duration")]
        public IActionResult GetByDuration(int duration)
        {
            try
            {
                List<Movie> ListByDuration = Movie.ReadByDuration(duration);
                if (ListByDuration == null)
                {
                    return BadRequest("Please insert Valid rating");
                }
                else
                {
                    return Ok(ListByDuration);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת
            }

        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] Movie m)
        {
            return Movie.Insert(m);
        }

        // POST api/values
        [HttpPost("Movie/{movieId}/Cas/{CastId}")]
        public int Post(int movieId, int CastId)
        {
            return Movie.Insertcast2Movie(CastId,movieId);
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

