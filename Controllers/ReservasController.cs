using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestaurantProjectAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantProjectAPI.Controllers
{
    // [controller] hace que se use el nombre del controlador, en este caso Reservas
    [EnableCors("ReglasCors")]
    [Route("GestionRestaurante/[controller]")] 
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private ContextoDB ElContextoDB;
        public ReservasController(ContextoDB dbcontexto)
        {
            ElContextoDB = dbcontexto;
        }

        // GET: api/<ReservasController>
        [HttpGet]
        public ActionResult<IEnumerable<Reservas>> ObtenerLista() 
        {
            try
            {
                return Ok(ElContextoDB.Reservas.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<ReservasController>/5
        [HttpGet("{id}")]
        public Reservas Get(int id) 
        {
            try
            {
                foreach (var reserva in ElContextoDB.Reservas)
                {
                    if (reserva.id == id) return reserva;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST api/<ReservasController>
        [HttpPost]
        public ActionResult Post([FromBody] Reservas reserva)
        {
            try
            {
                ElContextoDB.Reservas.Add(reserva);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                // Ok indica peticion con respuesta codigo 200 y su cuerpo es la reserva
                return Ok(reserva);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<ReservasController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Reservas reserva)
        {
            try
            {
                Reservas reservaAEditar = Get(reserva.id);

                reservaAEditar.nombreCliente = reserva.nombreCliente;
                reservaAEditar.fechaYhora = reserva.fechaYhora;
                reservaAEditar.numPersonas = reserva.numPersonas;

                ElContextoDB.Reservas.Update(reservaAEditar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                return Ok(reservaAEditar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<ReservasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Reservas reservaAEliminar = Get(id);

                ElContextoDB.Reservas.Remove(reservaAEliminar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos

                return Ok(reservaAEliminar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
