using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Registrazioni.Repository.Model;

namespace Registrazioni.Repository
{
    public class RegistrazioniDbContext(DbContextOptions<RegistrazioniDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cane>().HasKey(c => c.Id);
            modelBuilder.Entity<Cane>().Property(c => c.Razza).IsRequired();
        }
        public DbSet<Cane> Cani { get; set; } // Tabella Cani

        public DbSet<Votazione> Votazioni { get; set; } // Tabella Votazioni

        public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; } // Tabella TransactionalOutboxes

    }
}
