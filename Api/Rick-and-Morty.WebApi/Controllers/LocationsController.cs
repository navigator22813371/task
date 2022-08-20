using Microsoft.AspNetCore.Mvc;
using Rick_and_Morty.Application.Logics.Locations.Command.Create;
using Rick_and_Morty.Application.Logics.Locations.Command.Delete;
using Rick_and_Morty.Application.Logics.Locations.Command.Update;
using Rick_and_Morty.Application.Logics.Locations.Queries.Filter;
using Rick_and_Morty.Application.Logics.Locations.Queries.GetAll;
using Rick_and_Morty.Application.Logics.Locations.Queries.GetById;
using Rick_and_Morty.Application.Logics.Locations.Queries.GetByName;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Controllers
{
    public class LocationsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllLocationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdLocationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] FilterLocationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
