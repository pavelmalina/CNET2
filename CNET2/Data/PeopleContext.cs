﻿using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class PeopleContext : DbContext
    {
        // Tohle musí být property, jinak nešlape
        public DbSet<Person> Persons { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PeopleDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}
