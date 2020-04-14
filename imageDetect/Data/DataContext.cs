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
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Photos> Photos { get; set; }

        public DbSet<Statistics> Statistics { get; set; }

    }
}
