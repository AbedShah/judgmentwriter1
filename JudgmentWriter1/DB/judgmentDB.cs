using JudgmentWriter1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JudgmentWriter1.DB
{
    public class judgmentDB : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Judgment>().ToTable("Judgments");

        }
        public DbSet<Judgment> Judgments { get; set; }
    }
}