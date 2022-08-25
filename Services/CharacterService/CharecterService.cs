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


        public async Task<ServiceResponse<List<Charecter>>> AddCharacter(Charecter newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Charecter>>();
            charecters.Add(newCharacter);
            serviceResponse.Data = charecters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Charecter>>> GetAllCharacters()
        {
            return new ServiceResponse<List<Charecter>>{Data = charecters};
        }

        public async Task<ServiceResponse<Charecter>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Charecter>();
            var character = charecters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;
        }
    }
}