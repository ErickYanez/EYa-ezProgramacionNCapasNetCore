using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(aseguradora);
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultusuario = BL.Usuario.GetAllEF(usuario);
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();

            aseguradora.Usuario = new ML.Usuario();
            if (IdAseguradora == null)
            {
                aseguradora.Usuario.Usuarios = resultusuario.Objects;
                return View(aseguradora);
            }
            else
            {
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Objeto;
                    aseguradora.Usuario.Usuarios = resultusuario.Objects;
                    return View(aseguradora);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar la aseguradora";
                }
                return View(aseguradora);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            //ADD
            if (aseguradora.IdAseguradora == 0)
            {
                ML.Result result = BL.Aseguradora.Add(aseguradora);
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
                if (aseguradora != null)
                {
                    ML.Result result = BL.Aseguradora.Update(aseguradora);
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

        //[HttpDelete]
        public ActionResult Delete(int idAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if (aseguradora != null)
            {
                ML.Result result = BL.Aseguradora.Delete(idAseguradora);
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
                return Redirect("/Aseguradora/GetAll");
            }
            return PartialView("Modal");
        }
    }
}
