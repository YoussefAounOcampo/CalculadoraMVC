using CalculadoraMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CalculadoraMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } // Nueva propiedad

    }
}
