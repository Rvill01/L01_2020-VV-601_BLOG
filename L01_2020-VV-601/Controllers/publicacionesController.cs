using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_VV_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_VV_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogContext _publicacionesContext;
        public publicacionesController(blogContext publicacionesContext)
        {
            _publicacionesContext = publicacionesContext;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<publicaciones> listpub = (from e in _publicacionesContext.publicaciones
                                             select e).ToList();
            if (listpub.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listpub);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardar([FromBody] publicaciones publicacion)
        {
            try
            {
                _publicacionesContext.publicaciones.Add(publicacion);
                _publicacionesContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult Actualizar(int id, [FromBody] publicaciones publiMod)
        {
            publicaciones? publiAc = (from e in _publicacionesContext.publicaciones
                                   where e.publicacionId == id
                                   select e).FirstOrDefault();
            if (publiAc == null)
            {
                return NotFound();

            }
            publiAc.publicacionId = publiMod.publicacionId;
            publiAc.titulo = publiMod.titulo;
            publiAc.descripcion = publiMod.descripcion;

            _publicacionesContext.Entry(publiAc).State = EntityState.Modified;
            _publicacionesContext.SaveChanges();
            return Ok(publiMod);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult eliminar(int id)
        {
            publicaciones? publicacion = (from e in _publicacionesContext.publicaciones
                                 where e.publicacionId == id
                                 select e).FirstOrDefault();
            if (publicacion == null)
            {
                return NotFound();

            }
            _publicacionesContext.publicaciones.Attach(publicacion);
            _publicacionesContext.publicaciones.Remove(publicacion);
            _publicacionesContext.SaveChanges();
            return Ok(publicacion);
        }


    }
}
