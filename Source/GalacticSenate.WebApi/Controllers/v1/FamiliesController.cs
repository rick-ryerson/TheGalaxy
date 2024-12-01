using Celestial.WebApi.Core.Attributes;
using GalacticSenate.WebApi.Requests.Families;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GalacticSenate.WebApi.Controllers.v1 {
    [BaseResponseController]
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase {
        // GET: api/<FamiliesController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FamiliesController>/5
        [HttpGet("{id}")]
        public string GetFamily(Guid id) {
            return "value";
        }

        // POST api/<FamiliesController>
        [HttpPost]
        public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyRequest request) {

            // return CreatedAtAction(nameof(GetFamily), new { id = familyId }, response);
            throw new NotImplementedException();
        }

        // PUT api/<FamiliesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<FamiliesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
