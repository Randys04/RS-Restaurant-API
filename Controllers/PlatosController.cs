using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestaurantProjectAPI.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantProjectAPI.Controllers
{
    // [controller] hace que se use el nombre del controlador, en este caso Platos
    [EnableCors("ReglasCors")]
    [Route("GestionRestaurante/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private ContextoDB ElContextoDB;
        public PlatosController(ContextoDB dbcontexto)
        {
            ElContextoDB = dbcontexto;
        }


        // GET: api/<PlatosController>
        [HttpGet]
        public ActionResult<IEnumerable<Platos>> ObtenerLista()
        {
            try
            {
                return Ok(ElContextoDB.Platos.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<PlatosController>/5
        [HttpGet("{id}")]
        public Platos Get(int id) 
        {

            try
            {
                foreach (var plato in ElContextoDB.Platos)
                {
                    if (plato.id == id) return plato;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST api/<PlatosController>
        [HttpPost]
        public ActionResult Post([FromBody] Platos plato)
        {
            try
            {
                ElContextoDB.Platos.Add(plato);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                // Ok indica peticion con respuesta codigo 200 y su cuerpo es el plato
                return Ok(plato);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<PlatosController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Platos plato) 
        {
            try
            {
                Platos platoAEditar = Get(plato.id);

                platoAEditar.nombre = plato.nombre;
                platoAEditar.categoria = plato.categoria;
                platoAEditar.descripcion = plato.descripcion;
                platoAEditar.precio = plato.precio;
                platoAEditar.imagen = plato.imagen;

                ElContextoDB.Platos.Update(platoAEditar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                return Ok(platoAEditar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<PlatosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Platos platoAEliminar = Get(id);

                ElContextoDB.Platos.Remove(platoAEliminar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos

                return Ok(platoAEliminar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
