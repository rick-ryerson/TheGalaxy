using GalacticSenate.Library.Services.PersonNameType;
using GalacticSenate.Library.Services.PersonNameType.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GalacticSenate.WebApi.Controllers.v1 {
   [Route("api/v1/[controller]")]
   [ApiController]
   public class PersonNameTypeController : ControllerBase {
      private readonly IPersonNameTypeService personNameTypeService;

      public PersonNameTypeController(IPersonNameTypeService personNameTypeService) {
         this.personNameTypeService = personNameTypeService ?? throw new ArgumentNullException(nameof(personNameTypeService));
      }

      // GET: api/<PersonNameTypeController>
      [HttpGet]
      public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue) {
         try {
            return Ok(await personNameTypeService.ReadAsync(new ReadPersonNameTypeMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // GET api/<PersonNameTypeController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id) {
         try {
            return Ok(await personNameTypeService.ReadAsync(new ReadPersonNameTypeRequest { Id = id }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // POST api/<PersonNameTypeController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] string value) {
         try {
            return Ok(await personNameTypeService.AddAsync(new AddPersonNameTypeRequest { Value = value }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // PUT api/<PersonNameTypeController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] string value) {
         try {
            return Ok(await personNameTypeService.UpdateAsync(new UpdatePersonNameTypeRequest { Id = id, NewValue = value }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // DELETE api/<PersonNameTypeController>/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteAsync(int id) {
         try {
            return Ok(await personNameTypeService.DeleteAsync(new DeletePersonNameTypeRequest { Id = id }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

   }
}
