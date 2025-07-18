using Guidici.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Repository
{
    public class GuidiciDbContext(DbContextOptions<GuidiciDbContext> dbContextOptions) : DbContext(dbContextOptions) 
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasKey(p => p.Id);
            modelBuilder.Entity<TransactionalOutbox>().HasKey(e => new { e.Id });
            modelBuilder.Entity<TransactionalOutbox>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<VotazioneKafka>().HasKey(v => v.Id);

        }

        public DbSet<Persona> Persone { get; set; }

        public DbSet<VotazioneKafka> VotazioniKafka { get; set; }

        public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; }


    }
}
