using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using ML;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result resultempresa = BL.Empresa.GetAllEF(empresa);
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            if (NumeroEmpleado == null)
            {
                empleado.Empresa.Empresas = resultempresa.Objects;
                return View(empleado);
            }
            else
            {
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Objeto;
                    empleado.Empresa.Empresas = resultempresa.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar el usuario";
                }
                return View(empleado);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile image = Request.Form.Files["FileImage"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }
            //ADD
            ML.Result result = BL.Empleado.GetById(empleado.NumeroEmpleado);
            if (result.Objeto == null)
            {
                result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            else
            {
                //UPDATE
                if (empleado != null)
                {
                    result = BL.Empleado.Update(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = result.Message;
                    }
                    else
                    {
                        ViewBag.Message = "Error: " + result.Message;
                    }
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(ML.Empleado empleado)
        {
            if (empleado != null)
            {
                ML.Result result = BL.Empleado.Delete(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            else
            {
                return Redirect("/Empleado/GetAll");
            }
            return PartialView("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile foto)
        {

            using var fileStream = foto.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
