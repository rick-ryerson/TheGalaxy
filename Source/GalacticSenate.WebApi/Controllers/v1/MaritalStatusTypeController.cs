using GalacticSenate.Library.Requests;
using GalacticSenate.Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GalacticSenate.WebApi.Controllers.v1 {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaritalStatusTypeController : ControllerBase {
        private readonly IMaritalStatusTypeService genderService;

        public MaritalStatusTypeController(IMaritalStatusTypeService genderService) {
            this.genderService = genderService ?? throw new ArgumentNullException(nameof(genderService));
        }

        // GET: api/<MaritalStatusTypeController>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue) {
            try {
                return Ok(await genderService.ReadAsync(new ReadMaritalStatusTypeMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }

        // GET api/<MaritalStatusTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            try {
                return Ok(await genderService.ReadAsync(new ReadMaritalStatusTypeRequest { Id = id }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }

        // POST api/<MaritalStatusTypeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value) {
            try {
                return Ok(await genderService.AddAsync(new AddMaritalStatusTypeRequest { Value = value }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }

        // PUT api/<MaritalStatusTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value) {
            try {
                return Ok(await genderService.UpdateAsync(new UpdateMaritalStatusTypeRequest { Id = id, NewValue = value }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }

        // DELETE api/<MaritalStatusTypeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                return Ok(await genderService.DeleteAsync(new DeleteMaritalStatusTypeRequest { Id = id }));
            }
            catch (Exception ex) {
                return Problem(detail: ex.Message);
            }
        }
    }
}
