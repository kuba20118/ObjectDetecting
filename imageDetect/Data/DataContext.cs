using imageDetect;
using Microsoft.EntityFrameworkCore;
using System;
using imageDetect.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageDetect.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;port=3306;user=user;password=password;database=db;");
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Photos> Photos { get; set; }

        public DbSet<Statistics> Statistics { get; set; }

    }
}
