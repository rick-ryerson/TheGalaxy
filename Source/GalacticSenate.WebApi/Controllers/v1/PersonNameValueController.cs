using GalacticSenate.Library.Services.PersonNameValue;
using GalacticSenate.Library.Services.PersonNameValue.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GalacticSenate.WebApi.Controllers.v1 {
   [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonNameValueController : ControllerBase
    {
        private readonly IPersonNameValueService personNameValueService;

        public PersonNameValueController(IPersonNameValueService personNameValueService)
        {
            this.personNameValueService = personNameValueService ?? throw new ArgumentNullException(nameof(personNameValueService));
        }

        // GET: api/<PersonNameValueController>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                return Ok(await personNameValueService.ReadAsync(new ReadPersonNameValueMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // GET api/<PersonNameValueController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await personNameValueService.ReadAsync(new ReadPersonNameValueRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // POST api/<PersonNameValueController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                return Ok(await personNameValueService.AddAsync(new AddPersonNameValueRequest { Value = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // PUT api/<PersonNameValueController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                return Ok(await personNameValueService.UpdateAsync(new UpdatePersonNameValueRequest { Id = id, NewValue = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // DELETE api/<PersonNameValueController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await personNameValueService.DeleteAsync(new DeletePersonNameValueRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }
    }
}
