using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Data
{
   public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
        }

        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            // ToTable(table) method is used to define the Table name for Entity Class, in this case we are creating Genre table. Equivalent to the Table attribute
            builder.ToTable("Genre");

            // HasKey(selector) method takes lambda expression that selects the primary key for our Table, in our case we want Id as primary Key. It is similar to [Key] attribute in dataannotations.
            builder.HasKey(g => g.Id);

            // Property(selector) is used to describe more details about property or column in our table, like making it not null or restricting the maximum length etc and many more.
            builder.Property(g => g.Name).IsRequired().HasMaxLength(64);
        }
    }
}
