using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharecterService : ICharacterService
    {
        private static List<Charecter> charecters = new List<Charecter>{
            new Charecter(),
            new Charecter {Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CharecterService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Charecter character = _mapper.Map<Charecter>(newCharacter);
            character.Id = charecters.Max(c => c.Id) + 1;
            charecters.Add(character);
            charecters.Add(_mapper.Map<Charecter>(newCharacter));
            serviceResponse.Data = charecters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = charecters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = charecters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }
    }
}