using GalacticSenate.Library;
using GalacticSenate.Library.Party;
using GalacticSenate.Library.Party.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = GalacticSenate.Domain.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GalacticSenate.WebApi.Controllers.v1 {
   [Route("api/v1/[controller]")]
   [ApiController]
   public class PartyController : ControllerBase {
      private readonly IPartyService partyService;

      public PartyController(IPartyService partyService) {
         this.partyService = partyService ?? throw new ArgumentNullException(nameof(partyService));
      }

      // GET: api/<PartyController>
      [HttpGet]
      public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = int.MaxValue) {
         try {
            return Ok(await partyService.ReadAsync(new ReadPartyMultiRequest { PageIndex = pageIndex, PageSize = pageSize }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // GET api/<PartyController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> Get(Guid id) {
         try {
            return Ok(await partyService.ReadAsync(new ReadPartyRequest { Id = id }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // POST api/<PartyController>
      [HttpPost]
      public async Task<IActionResult> Post() {
         try {
            return Ok(await partyService.AddAsync(new AddPartyRequest { Id = Guid.NewGuid() }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }

      // DELETE api/<PartyController>/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteAsync(Guid id) {
         try {
            return Ok(await partyService.DeleteAsync(new DeletePartyRequest { Id = id }));
         } catch (Exception ex) {
            return Problem(detail: ex.Message);
         }
      }
   }
}
