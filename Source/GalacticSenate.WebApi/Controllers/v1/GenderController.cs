using GalacticSenate.Library;
using GalacticSenate.Library.Gender;
using GalacticSenate.Library.Gender.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GalacticSenate.WebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService genderService;

        public GenderController(IGenderService genderService)
        {
            this.genderService = genderService ?? throw new ArgumentNullException(nameof(genderService));
        }

        // GET: api/<GenderController>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                return Ok(genderService.Read(new ReadGenderMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // GET api/<GenderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(genderService.Read(new ReadGenderRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // POST api/<GenderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                return Ok(genderService.Add(new AddGenderRequest { Value = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // PUT api/<GenderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                return Ok(genderService.Update(new UpdateGenderRequest { Id = id, NewValue = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // DELETE api/<GenderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(genderService.Delete(new DeleteGenderRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }
    }
}
