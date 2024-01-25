using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCProje.Models.Domain;

namespace MVCProje.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Oyunlar> Oyunlars { get; set; }
        public DbSet<Tur> Turs { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Sepet> Sepets { get; set; }

        public DbSet<SepetToplam> SepetToplams { get; set; }


    }
}
