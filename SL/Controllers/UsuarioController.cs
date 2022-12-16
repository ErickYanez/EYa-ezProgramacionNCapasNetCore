using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            //return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{idUsuario}")]
        public IActionResult GetById(byte idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<UsuarioController>
        //[HttpPost("GetAll")]
        //public IActionResult GetAll(string? nombre, string? apellidoPaterno, byte? idRol)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Nombre = nombre;
        //    usuario.ApellidoPaterno = apellidoPaterno;
        //    usuario.Rol = new ML.Rol();
        //    usuario.Rol.IdRol = (byte)((idRol == null) ? 0 : idRol);
        //    ML.Result result = BL.Usuario.GetAllEF(usuario);
        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost("GetAll")]
        public IActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody] ML.Usuario usuario)
        {
            if (usuario.IdUsuario != null && usuario.IdUsuario != 0)
            {
                ML.Result result = BL.Usuario.UpdateEF(usuario);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest("Especifica el Id del objeto a actualizar");
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(byte idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetByIdUserName/{userName}")]
        public IActionResult GetByIdUserName(string userName)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetByIdUserName(userName);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
