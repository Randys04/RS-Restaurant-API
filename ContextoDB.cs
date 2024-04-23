using Microsoft.EntityFrameworkCore;
using RestaurantProjectAPI.Models;

namespace RestaurantProjectAPI
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions<ContextoDB> opciones) : base(opciones)
        {

        }

        public DbSet<Platos> Platos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
    }
}

