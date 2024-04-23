using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestaurantProjectAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantProjectAPI.Controllers
{
    // [controller] hace que se use el nombre del controlador, en este caso Ventas
    [EnableCors("ReglasCors")]
    [Route("GestionRestaurante/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private ContextoDB ElContextoDB;
        public VentasController(ContextoDB dbcontexto)
        {
            ElContextoDB = dbcontexto;
        }


        // funcion para retornar la lista de ventas qu esten dentro de un rango de fechas que se termina a partir de las fechas que recibe
        // GET: api/<VentasController>
        [HttpGet("Rangofechas")]
        public ActionResult<IEnumerable<Ventas>> ObtenerListaRangoFechas(DateTime inicio, DateTime fin)
        {
            try
            {
                List<Ventas> ventasRango = new List<Ventas>();

                foreach (var venta in ElContextoDB.Ventas)
                {
                    if (venta.fechaYhora >= inicio && venta.fechaYhora <= fin)
                    {
                        ventasRango.Add(venta);
                    }
                }

                return ventasRango;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //funcion que retorna una lista de ventas que esten en el mes que recibe 
        // GET: api/<VentasController>
        [HttpGet("RangoMes")]
        public ActionResult<IEnumerable<Ventas>> ObtenerListaRangoMes(DateTime mes) // funcion para obtener la lista de ventas del cache
        {
            try
            {
                List<Ventas> ventasRango = new List<Ventas>();

                foreach (var venta in ElContextoDB.Ventas)
                {
                    if (venta.fechaYhora.ToString("yyyy-MM") == mes.ToString("yyyy-MM"))
                    {
                        ventasRango.Add(venta);
                    }
                }

                return ventasRango;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //funcion que retorna una lista de ventas que esten en el dia que recibe
        // GET: api/<VentasController>
        [HttpGet("RangoDia")]
        public ActionResult<IEnumerable<Ventas>> ObtenerListaRangoDia(DateTime dia) // funcion para obtener la lista de ventas del cache
        {
            try
            {
                List<Ventas> ventasRango = new List<Ventas>();

                foreach (var venta in ElContextoDB.Ventas)
                {
                    if (venta.fechaYhora.ToString("yyyy-MM-dd") == dia.ToString("yyyy-MM-dd"))
                    {
                        ventasRango.Add(venta);
                    }

                }

                return ventasRango;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/<VentasController>
        [HttpGet]
        public ActionResult<IEnumerable<Ventas>> ObtenerLista() 
        {
            try
            {
                return Ok(ElContextoDB.Ventas.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<VentasController>/5
        [HttpGet("{id}")]
        public Ventas Get(int id) 
        {
            try
            {
                foreach (var venta in ElContextoDB.Ventas)
                {
                    if (venta.id == id) return venta;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST api/<VentasController>
        [HttpPost]
        public ActionResult Post([FromBody] Ventas venta)
        {
            try
            {
                ElContextoDB.Ventas.Add(venta);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                // Ok indica peticion con respuesta codigo 200 y su cuerpo es la venta
                return Ok(venta);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<VentasController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Ventas venta)
        {
            try
            {
                Ventas ventaAEditar = Get(venta.id);

                ventaAEditar.fechaYhora = venta.fechaYhora;
                ventaAEditar.platos = venta.platos;
                ventaAEditar.cantidad = venta.cantidad;

                ElContextoDB.Ventas.Update(ventaAEditar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                return Ok(ventaAEditar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<VentasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Ventas ventaAEliminar = Get(id);

                ElContextoDB.Ventas.Remove(ventaAEliminar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos

                return Ok(ventaAEliminar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
