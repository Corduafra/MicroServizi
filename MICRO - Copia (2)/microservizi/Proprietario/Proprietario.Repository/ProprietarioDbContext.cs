using Microsoft.EntityFrameworkCore;
using Proprietario.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proprietario.Repository
{
    public class ProprietarioDbContext(DbContextOptions<ProprietarioDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder) {         
            
            
            
            modelBuilder.Entity<Model.Soggetto>().HasKey(x => x.Id);
            modelBuilder.Entity<Model.CaneProprietario>().HasKey(x => x.Id);



        }

        public DbSet<Soggetto> Soggetti { get; set; }
        public DbSet<CaneProprietario> CaneProprietari { get; set; }




    }

    
}
