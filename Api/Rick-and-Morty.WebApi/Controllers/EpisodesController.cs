using Microsoft.AspNetCore.Mvc;
using Rick_and_Morty.Application.Logics.Episodes.Command.Create;
using Rick_and_Morty.Application.Logics.Episodes.Command.Delete;
using Rick_and_Morty.Application.Logics.Episodes.Command.Update;
using Rick_and_Morty.Application.Logics.Episodes.Queries.GetAll;
using Rick_and_Morty.Application.Logics.Episodes.Queries.GetById;
using Rick_and_Morty.Application.Logics.Episodes.Queries.GetByName;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Controllers
{
    public class EpisodesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]GetAllEpisodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetByIdEpisodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] GetByNameEpisodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEpisodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]DeleteEpisodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEpisodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharactersInEpisode(AddCharactersInEpisodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharactersFromEpisode(
            DeleteCharactersFromEpisodeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
