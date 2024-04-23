using System.ComponentModel.DataAnnotations;

namespace RestaurantProjectAPI.Models
{
    public class Clientes
    {
        [Key]
        public String cedula { get; set; }
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public String? tipo { get; set;}
    }
}
