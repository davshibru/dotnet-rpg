using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Charecter, GetCharacterDto>();
            CreateMap<AddCharacterDto, Charecter>();
            CreateMap<UpdateCharacterDto, Charecter>();
            CreateMap<Weapon, GetWeaponDto>();
        }
    }
}