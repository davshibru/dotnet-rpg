using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharecterController : ControllerBase
    {
        private readonly ICharacterService characterService;
        public CharecterController(ICharacterService characterService)
        {
            this.characterService = characterService;
            
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Charecter>>>> Get() 
        {
            return Ok(await characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Charecter>>> GetSingle(int id) 
        {
            return Ok(await characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Charecter>>>> AddCharecter(Charecter newCharecter)
        {
            return Ok(await characterService.AddCharacter(newCharecter));
        }
    }
}