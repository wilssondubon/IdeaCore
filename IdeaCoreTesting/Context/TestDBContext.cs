using IdeaCoreTesting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreTesting.Context
{
    public partial class TestDBContext : DbContext
    {
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Electrodomestico> Electrodomesticos { get; set; }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.ElectrodomesticoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TipoConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
