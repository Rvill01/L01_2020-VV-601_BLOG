using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_VV_601.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace L01_2020_VV_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly blogContext _usuarioContext;
        public usuarioController(blogContext usuarioContext)
        {
            _usuarioContext = usuarioContext;

        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuario = (from e in _usuarioContext.usuarios
                                           select e).ToList();
            if (listadoUsuario.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuario);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardar([FromBody] usuarios usuario)
        {
            try
            {
                _usuarioContext.usuarios.Add(usuario);
                _usuarioContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult Actualizar(int id, [FromBody] usuarios usuarioMod)
        {
            usuarios? usuarioAc = (from e in _usuarioContext.usuarios
                                     where e.usuarioId == id
                                     select e).FirstOrDefault();
            if (usuarioAc == null)
            {
                return NotFound();

            }
            usuarioAc.nombreUsuario = usuarioMod.nombreUsuario;
            usuarioAc.clave = usuarioMod.clave;
            usuarioAc.nombre = usuarioMod.nombre;
            usuarioAc.apellido = usuarioMod.apellido;

            _usuarioContext.Entry(usuarioAc).State = EntityState.Modified;
            _usuarioContext.SaveChanges();
            return Ok(usuarioMod);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult eliminar(int id)
        {
            usuarios? usuario = (from e in _usuarioContext.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();

            }
            _usuarioContext.usuarios.Attach(usuario);
            _usuarioContext.usuarios.Remove(usuario);
            _usuarioContext.SaveChanges();
            return Ok(usuario);
        }

        [HttpGet]
        [Route("Find/{rol}")]
        public IActionResult findRol(int rol)
        {
            usuarios? usuario = (from e in _usuarioContext.usuarios
                               where e.rolId ==rol
                               select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("Find/{nombre}/{apellido}")]
        public IActionResult findNomAp(string nombre, string apellido)
        {
            usuarios? usuario = (from e in _usuarioContext.usuarios
                                 where e.nombre.Contains(nombre) && e.apellido.Contains(apellido)
                               select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
    }
}
