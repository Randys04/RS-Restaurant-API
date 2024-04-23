using System.ComponentModel.DataAnnotations;

namespace RestaurantProjectAPI.Models
{
    public class Ventas
    {
        [Key]
        public int id { get; set; } 
        public DateTime fechaYhora { get; set; }
        public String platos { get; set; }
        public int cantidad { get; set; }
        
    }
}
