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



    }
}
