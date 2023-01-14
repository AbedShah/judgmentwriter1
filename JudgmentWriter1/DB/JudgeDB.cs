
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using JudgmentWriter1.Models;

namespace JudgmentWriter1.DB
{
    public class JudgeDB : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Judge>().ToTable("Judges");

        }
        public DbSet<Judge> Judges { get; set; }
    }
}


