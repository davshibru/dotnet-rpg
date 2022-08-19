using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharecterService : ICharacterService
    {
        private static List<Charecter> charecters = new List<Charecter>{
            new Charecter(),
            new Charecter {Id = 1, Name = "Sam"}
        };


        public async Task<List<Charecter>> AddCharacter(Charecter newCharacter)
        {
            charecters.Add(newCharacter);
            return charecters;
        }

        public async Task<List<Charecter>> GetAllCharacters()
        {
            return charecters;
        }

        public async Task<Charecter> GetCharacterById(int id)
        {
            return charecters.FirstOrDefault(c => c.Id == id);
        }
    }
}