using Microsoft.AspNetCore.Mvc;
using Rick_and_Morty.Application.Logics.Characters.Command.Create;
using Rick_and_Morty.Application.Logics.Characters.Command.Delete;
using Rick_and_Morty.Application.Logics.Characters.Command.Update;
using Rick_and_Morty.Application.Logics.Characters.Queries.Filter;
using Rick_and_Morty.Application.Logics.Characters.Queries.GetAll;
using Rick_and_Morty.Application.Logics.Characters.Queries.GetById;
using Rick_and_Morty.Application.Logics.Characters.Queries.GetByName;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Controllers
{
    public class CharactersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCharacterQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetByIdCharacterQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        //http://localhost:19461/api/Characters/Filter?Status[]=&Gender[]=
        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] FilterCharacterQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]DeleteCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
