using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public EmpresaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Empresa
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            //ML.Result result = BL.Empresa.GetAllEF(empresa);
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                string urlAPI = _configuration["UrlAPI"];

                using (var user = new HttpClient())
                {
                    user.BaseAddress = new Uri(urlAPI);

                    var responseTask = user.GetAsync("Empresa/GetAll");
                    //result = BL.Usuario.GetAllEF();

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Empresa resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empresa>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(empresa);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.GetAllEF(empresa);
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetByIdEF(IdEmpresa.Value);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Objeto;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar el usuario";
                }
                return View(empresa);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile image = Request.Form.Files["FileImage"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }
            //HttpPostedFileBase file = Request.Files["ImagenData"];

            //if (file.ContentLength > 0)
            //{
            //    empresa.Logo = ConvertToBytes(file);
            //}
            //ADD
            if (empresa.IdEmpresa == 0)
            {
                ML.Result result = BL.Empresa.AddEF(empresa);
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
                if (empresa != null)
                {
                    ML.Result result = BL.Empresa.UpdateEF(empresa);
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
        public ActionResult Delete(int idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (empresa != null)
            {
                ML.Result result = BL.Empresa.DeleteEF(idEmpresa);
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
                return Redirect("/Empresa/GetAll");
            }
            return PartialView("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile logo)
        {

            using var fileStream = logo.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        //public byte[] ConvertToBytes(HttpPostedFileBase logo)
        //{
        //    byte[] data = null;
        //    System.IO.BinaryReader reader = new System.IO.BinaryReader(logo.InputStream);
        //    data = reader.ReadBytes((int)logo.ContentLength);
        //    return data;
        //}
    }
}
