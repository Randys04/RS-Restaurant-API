using System.ComponentModel.DataAnnotations;

namespace RestaurantProjectAPI.Models
{
    public class Reservas
    {
        [Key]
        public int id { get; set; } 
        public String nombreCliente { get; set; }
        public DateTime fechaYhora { get; set; }
        public int numPersonas { get; set; }
    }
}
