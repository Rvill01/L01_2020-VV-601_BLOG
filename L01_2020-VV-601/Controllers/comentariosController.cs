using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_VV_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_VV_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _comentarioContext;
        public comentariosController(blogContext comentarioContext)
        {
            _comentarioContext = comentarioContext;

        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listcom = (from e in _comentarioContext.comentarios
                                           select e).ToList();
            if (listcom.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listcom);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardar([FromBody] comentarios comentarios)
        {
            try
            {
                _comentarioContext.comentarios.Add(comentarios);
                _comentarioContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult Actualizar(int id, [FromBody] comentarios comMod)
        {
            comentarios? comAc = (from e in _comentarioContext.comentarios
                                      where e.cometarioId == id
                                      select e).FirstOrDefault();
            if (comAc == null)
            {
                return NotFound();

            }
            comAc.comentario = comMod.comentario;
           

            _comentarioContext.Entry(comAc).State = EntityState.Modified;
            _comentarioContext.SaveChanges();
            return Ok(comMod);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult eliminar(int id)
        {
            comentarios? comentario = (from e in _comentarioContext.comentarios
                                          where e.cometarioId == id
                                          select e).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();

            }
            _comentarioContext.Attach(comentario);
            _comentarioContext.comentarios.Remove(comentario);
            _comentarioContext.comentarios.Remove(comentario);
            _comentarioContext.SaveChanges();
            return Ok(comentario);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult comIdPub(int id)
        {
            List<comentarios> listCom = (from e in _comentarioContext.comentarios
                                         where e.publicacionId == id
                                         select e).ToList();
            if (listCom == null)
            {
                return NotFound();
            }
            return Ok(listCom);
        }


    }
}
