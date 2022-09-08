using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
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

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strenght));
                damage -= new Random().Next(opponent.Defeats);

                if (damage > 0)
                    opponent.HitPoints -= damage;

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
    }
}