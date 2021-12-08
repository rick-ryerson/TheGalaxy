using GalacticSenate.Library;
using GalacticSenate.Library.OrganizationNameValue;
using GalacticSenate.Library.OrganizationNameValue.Requests;
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
    public class OrganizationNameValueController : ControllerBase
    {
        private readonly IOrganizationNameValueService organizationNameValueService;

        public OrganizationNameValueController(IOrganizationNameValueService organizationNameValueService)
        {
            this.organizationNameValueService = organizationNameValueService ?? throw new ArgumentNullException(nameof(organizationNameValueService));
        }

        // GET: api/<OrganizationNameValueController>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                return Ok(await organizationNameValueService.ReadAsync(new ReadOrganizationNameValueMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // GET api/<OrganizationNameValueController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await organizationNameValueService.ReadAsync(new ReadOrganizationNameValueRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // POST api/<OrganizationNameValueController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                return Ok(await organizationNameValueService.AddAsync(new AddOrganizationNameValueRequest { Value = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // PUT api/<OrganizationNameValueController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                return Ok(await organizationNameValueService.UpdateAsync(new UpdateOrganizationNameValueRequest { Id = id, NewValue = value }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }

        // DELETE api/<OrganizationNameValueController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await organizationNameValueService.DeleteAsync(new DeleteOrganizationNameValueRequest { Id = id }));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message);
            }
        }
    }
}
