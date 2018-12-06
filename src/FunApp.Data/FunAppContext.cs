﻿using FunApp.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FunApp.Data
{
    public class FunAppContext : IdentityDbContext<FunAppUser>
    {
        public FunAppContext(DbContextOptions<FunAppContext> options)
            : base(options)
        {
        }

        public DbSet<Joke> Jokes { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
