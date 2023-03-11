using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Models
{
    public class HoangTuanDB : DbContext
    {
        public HoangTuanDB(DbContextOptions<HoangTuanDB> options) : base(options)
        {
            // this. role
            //Identity<string>
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder moduleBuilder)
        {
            base.OnModelCreating(moduleBuilder);
        }

    }
    
}