using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Data
{

    public class UrlContext : DbContext
    {
        private string _connectionString;

        public UrlContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
    }

}
