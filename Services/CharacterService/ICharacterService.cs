using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Charecter>>> GetAllCharacters();
        Task<ServiceResponse<Charecter>> GetCharacterById(int id);
        Task<ServiceResponse<List<Charecter>>> AddCharacter(Charecter newCharacter);
    }
}