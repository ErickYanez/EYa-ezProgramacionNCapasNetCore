using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        // GET: api/<AseguradoraController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<AseguradoraController>/5
        [HttpGet("GetById/{idAseguradora}")]
        public IActionResult GetById(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.GetById(idAseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AseguradoraController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // PUT api/<AseguradoraController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody] ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora != null && aseguradora.IdAseguradora != 0)
            {
                ML.Result result = BL.Aseguradora.Update(aseguradora);
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

        // DELETE api/<AseguradoraController>/5
        [HttpDelete("Delete/{idAseguradora}")]
        public IActionResult Delete(int idAseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(idAseguradora);

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
