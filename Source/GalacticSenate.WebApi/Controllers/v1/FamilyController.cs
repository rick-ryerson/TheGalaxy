using Celestial.WebApi.Core.Attributes;
using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services;
using GalacticSenate.WebApi.Requests.Families;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GalacticSenate.WebApi.Controllers.v1 {
    [BaseResponseController]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase {
        private readonly IFamilyService familyService;

        public FamilyController(IFamilyService familyService) {
            this.familyService = familyService ?? throw new ArgumentNullException(nameof(familyService));
        }

        // GET: api/<FamiliesController>
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = int.MaxValue) {
            try {
                return Ok(await familyService.ReadAsync(new ReadFamilyMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }
        // GET api/<FamiliesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFamilyAsync(Guid id) {
            try {
                return Ok(await familyService.ReadAsync(new ReadFamilyRequest { Id = id }));

            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            };
        }

        // POST api/<FamiliesController>
        [HttpPost]
        public async Task<IActionResult> CreateFamily([FromBody] Requests.Families.CreateFamilyRequest request) {

            // return CreatedAtAction(nameof(GetFamily), new { id = familyId }, response);
            try {
                return Ok(await familyService.AddAsync(new Library.Requests.AddFamilyRequest { Id = request.Id, Name = request.Name }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
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
