using System.ComponentModel.DataAnnotations;

namespace RestaurantProjectAPI.Models
{
    public class Platos
    {
        [Key]
        public int id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public double precio { get; set; }
        public String categoria { get; set; }
        public String imagen { get; set; }
    }
}
