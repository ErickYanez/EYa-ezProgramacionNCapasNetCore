using Microsoft.AspNetCore.Mvc;
using ML;
using System.Reflection.Metadata.Ecma335;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];


            if (fileTXT != null)
            {

                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                string line;
                line = Textfile.ReadLine();

                ML.Result resultErrores = new ML.Result();
                resultErrores.Objects = new List<object>();

                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.Sexo = char.Parse(lines[3].Trim());
                    usuario.FechaNacimiento = lines[4];
                    usuario.Password = lines[5];
                    usuario.Telefono = lines[6];
                    usuario.Celular = lines[7];
                    usuario.CURP = lines[8];
                    usuario.UserName = lines[9];
                    usuario.Email = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = byte.Parse(lines[11]);
                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                    ML.Result result = BL.Usuario.AddEF(usuario);

                    if (!result.Correct)
                    {
                        resultErrores.Objects.Add(
                        "New file created: {0}" + usuario.FechaNacimiento +
                        "Author: Mahesh Chand" +
                        "Add one more line " +
                        "Add one more line " +
                        "Done! ");
                    }

                }

                if (resultErrores.Objects != null)
                {

                }
                TextWriter tx = new StreamWriter(@"C:\Users\digis\OneDrive\Documents\Erick Alejandro Yañez Aguilar\TXT\LayoutErrores.txt");
                foreach (string error in resultErrores.Objects)
                {
                    tx.WriteLine(error);
                }
                tx.Close();

            }
            return PartialView("Modal");
        }

        [HttpPost]
        public ActionResult CargaExcel(ML.Usuario usuario)
        {

            IFormFile archivo = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null) 
            {
                if (archivo.Length >= 0)
                {
                    string fileName = Path.GetFileName(archivo.FileName);
                    string folderPath = _configuration["PathFolder:Value"];
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if(extensionArchivo == extensionModulo)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) +"-"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using(FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            ML.Result resultUsuario = BL.Usuario.ConvertirExcelADataTable(connectionString);

                            if (resultUsuario.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuario.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View("CargaMasiva",resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                                return View("Modal");
                            }
                        }
                    }
                }

            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExcelADataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.AddEF(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.Message);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"C:\Users\digis\OneDrive\Documents\Erick Alejandro Yañez Aguilar\TXT\LayoutErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los usuarios no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los usuarios han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");
        }

    }
}
