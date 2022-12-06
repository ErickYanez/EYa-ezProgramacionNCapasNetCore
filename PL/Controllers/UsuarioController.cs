using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultrol = BL.Rol.GetAllEF();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            try
            {
                string urlAPI = _configuration["UrlAPI"];

                using (var user = new HttpClient())
                {
                    user.BaseAddress = new Uri(urlAPI);

                    var responseTask = user.GetAsync("Usuario/GetAll");
                    //result = BL.Usuario.GetAllEF();

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            if (result.Correct)
            {
                usuario.Rol.Roles = resultrol.Objects;
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAllEF(usuario);        
            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string urlAPI = _configuration["UrlAPI"];
                    httpClient.BaseAddress = new Uri(urlAPI);

                    var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/GetAll", usuario);
                    request.Wait();

                    var response = request.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = response.Content.ReadAsStringAsync().Result;

                        ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                        result.Message = resultAPI.Message;

                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in resultAPI.Objects)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                                result.Objects.Add(user);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            usuario.Rol = new ML.Rol();
            ML.Result resultrol = BL.Rol.GetAllEF();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultrol.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(usuario);
        }     

        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

        [HttpGet]
        public ActionResult Form(byte? IdUsuario)
        {
            ML.Result resultrol = BL.Rol.GetAllEF();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result resultpais = BL.Pais.GetAll();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (IdUsuario == null)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                usuario.Rol.Roles = resultrol.Objects;

                return View(usuario);
            }
            else
            {
                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                ML.Result result = new ML.Result();
                result.Objeto = new Object();
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string urlAPI = _configuration["UrlAPI"];
                        httpClient.BaseAddress = new Uri(urlAPI);

                        var request = httpClient.GetAsync($"Usuario/GetById/{IdUsuario.Value}");
                        request.Wait();

                        var response = request.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readContent = response.Content.ReadAsStringAsync().Result;

                            ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                            result.Message = resultAPI.Message;

                            if (resultAPI.Correct)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(resultAPI.Objeto.ToString());
                                result.Objeto = user;
                                result.Correct = true;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    result.Correct = false;
                    result.Message = error.Message;
                }
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Objeto;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;

                    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    //usuario.Direccion.Colonia.Municipio.Estado.Estados = (List<object>)GetEstado(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Data;
                    //usuario.Direccion.Colonia.Municipio.Municipios = (List<object>)GetMunicipio(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Data;
                    //usuario.Direccion.Colonia.Colonias = (List<object>)GetColonia(usuario.Direccion.Colonia.Municipio.IdMunicipio).Data;

                    usuario.Rol.Roles = resultrol.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar el usuario";
                }
                return View(usuario);
            }
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile image = Request.Form.Files["FileImage"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid)
            {
                
                //ADD
                if (usuario.IdUsuario == 0)
                {
                    //ML.Result result = BL.Usuario.AddEF(usuario);
                    ML.Result result = new ML.Result();                  
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            string urlAPI = _configuration["UrlAPI"];
                            httpClient.BaseAddress = new Uri(urlAPI);

                            var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                            request.Wait();

                            var response = request.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var readContent = response.Content.ReadAsStringAsync().Result;

                                ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                result.Message = resultAPI.Message;

                                if (resultAPI.Correct)
                                {
                                    result.Correct = true;
                                }
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
                    if (usuario != null)
                    {
                        //ML.Result result = BL.Usuario.UpdateEF(usuario);
                        ML.Result result = new ML.Result();
                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Clear();
                                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                string urlAPI = _configuration["UrlAPI"];
                                httpClient.BaseAddress = new Uri(urlAPI);


                                //Sending request to find web api REST service resource using HttpClient
                                var request = httpClient.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                                request.Wait();

                                //Checking the response is successful or not which is sent using HttpClient
                                var response = request.Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var readContent = response.Content.ReadAsStringAsync().Result;

                                    ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                    result.Message = resultAPI.Message;

                                    if (resultAPI.Correct)
                                    {
                                        result.Correct = true;
                                    }
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
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                ML.Result resultrol = BL.Rol.GetAllEF();

                ML.Result resultpais = BL.Pais.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Rol.Roles = resultrol.Objects;
                return View(usuario);
            }
            //HttpPostedFileBase file = Request.Files["ImagenData"];

            //if (file.ContentLength > 0)
            //{
            //    usuario.Imagen = ConvertToBytes(file);
            //}
            //ADD
        }

        //[HttpDelete]
        public ActionResult Delete(byte idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            if (usuario != null)
            {
                //ML.Result result = BL.Usuario.DeleteEF(idUsuario);
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string urlAPI = _configuration["UrlAPI"];
                        httpClient.BaseAddress = new Uri(urlAPI);

                        var request = httpClient.DeleteAsync($"Usuario/Delete/{idUsuario}");
                        request.Wait();

                        var response = request.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readContent = response.Content.ReadAsStringAsync().Result;

                            ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                            result.Message = resultAPI.Message;

                            if (resultAPI.Correct)
                            {
                                result.Correct = true;
                            }
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
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            else
            {
                return Redirect("/Usuario/GetAll");
            }
            return PartialView("Modal");
        }  

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        //public byte[] ConvertToBytes(HttpPostedFileBase imagen)
        //{
        //    byte[] data = null;
        //    System.IO.BinaryReader reader = new System.IO.BinaryReader(imagen.InputStream);
        //    data = reader.ReadBytes((int)imagen.ContentLength);
        //    return data;
        //}

        public JsonResult CambiarStatus(byte idUsuario, bool status)
        {
            var result = BL.Usuario.ChangeStatus(idUsuario, status);

            return Json(result.Objects);
        }
    }
}
