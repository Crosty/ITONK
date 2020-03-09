using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class BackendContext : DbContext
    {
        public BackendContext (DbContextOptions<BackendContext> options)
            : base(options)
        {
        }

        public DbSet<Backend.Models.Haandvaerker> Haandvaerker { get; set; }

        public DbSet<Backend.Models.Vaerktoej> Vaerktoej { get; set; }

        public DbSet<Backend.Models.Vaerktoejskasse> Vaerktoejskasse { get; set; }
    }
}
