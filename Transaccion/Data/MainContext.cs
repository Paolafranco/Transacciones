using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transaccion.Models;

namespace Transaccion.Data
{
    public class MainContext : DbContext
    {
        public MainContext (DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<Transaccion.Models.Cliente> Cliente { get; set; }
    }
}
