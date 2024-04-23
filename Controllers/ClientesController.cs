using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestaurantProjectAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantProjectAPI.Controllers
{
    // [controller] hace que se use el nombre del controlador, en este caso Clientes
    [EnableCors("ReglasCors")]
    [Route("GestionRestaurante/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private ContextoDB ElContextoDB;
        public ClientesController(ContextoDB dbcontexto)
        {
            ElContextoDB = dbcontexto;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public ActionResult<IEnumerable<Clientes>> ObtenerLista()
        {
            try
            {
                return Ok(ElContextoDB.Clientes.ToList());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        // GET api/<ClientesController>/5
        [HttpGet("{cedula}")]
        public Clientes Get(String cedula)
        {
            try
            {
                foreach (var cliente in ElContextoDB.Clientes)
                {
                    if (cliente.cedula == cedula) return cliente;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public ActionResult Post([FromBody] Clientes cliente)
        {
            try
            {
                cliente.tipo = "";
                ElContextoDB.Clientes.Add(cliente);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 
                // Ok indica peticion con respuesta codigo 200 y su cuerpo es el cliente
                return Ok(cliente);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{cedula}")]
        public ActionResult Put([FromBody] Clientes cliente) 
        {
            try
            {
                Clientes clienteAEditar = Get(cliente.cedula);

                clienteAEditar.nombre = cliente.nombre;
                clienteAEditar.apellidos = cliente.apellidos;

                ElContextoDB.Clientes.Update(clienteAEditar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos 

                return Ok(clienteAEditar);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{cedula}")]
        public ActionResult Delete(String cedula)
        {
            try
            {
                Clientes clienteAEliminar = Get(cedula);

                ElContextoDB.Clientes.Remove(clienteAEliminar);
                ElContextoDB.SaveChanges(); // se actualizan y guardan los cambios en la base de datos

                return Ok(clienteAEliminar);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
