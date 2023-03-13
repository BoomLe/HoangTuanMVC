using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HoangTuanMVC.Models
{
            public class HoangTuanDBFactory : IDesignTimeDbContextFactory<HoangTuanDB> 
    {

        public HoangTuanDB CreateDbContext(string[] args) 
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connecting = configuration.GetConnectionString("HoangTuanDB");

            var optionsBuilder = new DbContextOptionsBuilder<HoangTuanDB>();
            optionsBuilder.UseSqlServer(connecting);
            return new HoangTuanDB(optionsBuilder.Options);
           
        }
    }
    }
