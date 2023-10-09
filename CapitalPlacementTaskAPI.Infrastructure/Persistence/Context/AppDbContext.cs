using CapitalPlacementTaskAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace CapitalPlacementTaskAPI.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<ProgramDetail> ProgramDetails { get; set; }
        public virtual DbSet<AdditionalProgramInformation> AdditionalProgramInformations { get; set; }
    }
}
