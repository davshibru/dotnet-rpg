using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base(options)
        {
            
        }
        
        public DbSet<Charecter> Character => Set<Charecter>();
        public DbSet<User> Users {get; set;}
        public DbSet<Weapon> Weapons {get; set;}
        public DbSet<Skill> Skills { get; set; }
    }
}