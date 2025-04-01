using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;
using MiningStructuringDrug.Core.Application.DrugIndications.Queries;

namespace MiningStructuringDrug.API.Controllers
{
    [ApiController]
    [Route("api/drug-indications")]
    public class DrugIndicationController : ControllerBase
    {
        private readonly IMessageDispatcher _dispatcher;

        public DrugIndicationController(IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new ListDrugIndicationsQuery();
            var result = await _dispatcher.DispatchQuery<ListDrugIndicationsQuery, List<DrugIndicationDto>>(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDrugIndicationQuery { Id = id };
            var result = await _dispatcher.DispatchQuery<GetDrugIndicationQuery, DrugIndicationDto?>(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDrugIndicationCommand command)
        {
            await _dispatcher.DispatchCommand<CreateDrugIndicationCommand,DrugIndicationDto>(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDrugIndicationCommand command)
        {
            command.Id = id;
            await _dispatcher.Dispatch<UpdateDrugIndicationCommand>(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDrugIndicationCommand { Id = id };
            await _dispatcher.Dispatch<DeleteDrugIndicationCommand>(command);
            return NoContent();
        }
    }
}
