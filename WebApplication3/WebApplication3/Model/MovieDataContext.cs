using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using WebApplication3.Model;

namespace CoreServices.Models
{
    public partial class MovieDataContext : DbContext
    {
        private readonly MovieDataContext context;
        public MovieDataContext()
        {
        }

        public MovieDataContext(DbContextOptions<MovieDataContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });
        }
    }
}