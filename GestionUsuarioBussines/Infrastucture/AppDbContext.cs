using System;
using System.Collections.Generic;
using System.Net;
using GestionUsuarioBussines.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionUsuarioBussines.Infrastucture
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Domicilio> Domicilio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasOne(u => u.Domicilio).WithOne(d => d.Usuario).HasForeignKey<Domicilio>(d => d.IdUsuario); base.OnModelCreating(modelBuilder);
        }
    }
}
