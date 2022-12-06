using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
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
        public IActionResult GetAllDp(string? NumeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result result = BL.Dependiente.GetByIdEmpleado(NumeroEmpleado);
            if (result.Correct)
            {
                dependiente.Dependientes = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(dependiente);
        }

        [HttpGet]
        public ActionResult Form(int? idDependiente)
        {
            ML.Result resultEmpleado = BL.Empleado.GetAll();
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();
            dependiente.DependienteTipo = new ML.DependienteTipo();

            if (idDependiente == null)
            {
                dependiente.Empleado.Empleados = resultEmpleado.Objects;
                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                return View(dependiente);
            }
            else
            {
                ML.Result result = BL.Dependiente.GetById(idDependiente.Value);
                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Objeto;
                    dependiente.Empleado.Empleados = resultEmpleado.Objects;
                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar el dependiente";
                }
                return View(dependiente);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            //ADD
            if (dependiente.IdDependiente == 0)
            {
                ML.Result result = BL.Dependiente.Add(dependiente);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            //else
            {
                //UPDATE
                if (dependiente != null)
                {
                    ML.Result result = BL.Dependiente.Update(dependiente);
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

        public ActionResult Delete(ML.Dependiente dependiente)
        {
            if (dependiente != null)
            {
                ML.Result result = BL.Dependiente.Delete(dependiente);
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
                return Redirect("/Dependiente/GetAll");
            }
            return PartialView("Modal");
        }

    }
}
