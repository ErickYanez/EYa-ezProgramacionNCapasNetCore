using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        // GET: api/<EmpresaController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAllEF(empresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<EmpresaController>/5
        [HttpGet("GetById/{idEmpresa}")]
        public IActionResult GetById(int idEmpresa)
        {
            ML.Result result = BL.Empresa.GetByIdEF(idEmpresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.GetAllEF(empresa);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<EmpresaController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.AddEF(empresa);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // PUT api/<EmpresaController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody] ML.Empresa empresa)
        {
            if (empresa.IdEmpresa != null && empresa.IdEmpresa != 0)
            {
                ML.Result result = BL.Empresa.UpdateEF(empresa);
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

        // DELETE api/<EmpresaController>/5
        [HttpDelete("Delete/{idEmpresa}")]
        public IActionResult Delete(int idEmpresa)
        {
            ML.Result result = BL.Empresa.DeleteEF(idEmpresa);

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
