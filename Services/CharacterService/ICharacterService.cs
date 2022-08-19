using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Charecter>> GetAllCharacters();
        Task<Charecter> GetCharacterById(int id);
        Task<List<Charecter>> AddCharacter(Charecter newCharacter);
    }
}