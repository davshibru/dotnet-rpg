using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var characters = await _context.Character
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharactersIds.Contains(c.Id)).ToListAsync();

                bool defeated = false;

                while (!defeated)
                {
                    foreach (Charecter attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }

                        response.Data.Log
                            .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;

                            response.Data.Log
                                .Add($"{attacker.Name} win with {attacker.HitPoints} HP left");

                            break;
                        }
                    }
                }

            characters.ForEach(c => {
                c.Fight++;
                c.HitPoints = 100;
            });

            await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Seccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Character
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttakerId);

                var opponent = await _context.Character
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);

                if (skill == null)
                {
                    response.Seccess = false;
                    response.Message = $"{attacker.Name} doesn't know that skill.";
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoints,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Seccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int DoSkillAttack(Charecter? attacker, Charecter? opponent, Skill? skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Character
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttakerId);

                var opponent = await _context.Character
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = DoWeaponAttack(attacker, opponent);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoints,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Seccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int DoWeaponAttack(Charecter? attacker, Charecter? opponent)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strenght));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
            var characters = await _context.Character
                .Where(c => c.Fight > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var response = new ServiceResponse<List<HighScoreDto>>
            {
                Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
            };

            return response;
        }
    }
}