using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogicielNettoyagePC.Entities;

namespace LogicielNettoyagePC.Data
{
    internal class LogicielNettoyagePCContext : DbContext
    {
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Versions> Versions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseSqlite("Data source=LogicielNettoyagePC.db");
            }
        }

    }
}
