using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HoangTuanMVC.Models;
using App.Models;

namespace App.Models
{
    public class HoangTuanDB : DbContext
    {
        public DbSet<ContactModel> ContactModels{set;get;}
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

             //xóa những từ có tiền tố ASP
            foreach (var entity in moduleBuilder.Model.GetEntityTypes())
            {
                //lưu ý Get và Set => của Table
                var tableName = entity.GetTableName();
                if(tableName.StartsWith("AspNet"))
                {
                    entity.SetTableName(tableName.Substring(6));

                }
            }
            moduleBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(moduleBuilder);
        }

        public DbSet<App.Models.Category> Category { get; set; } = default!;

    }
    
}