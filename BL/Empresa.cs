using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result AddEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    //int query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}, '{empresa.Logo}'");
                    int query = context.Database.ExecuteSqlInterpolated($"EmpresaAdd @Nombre={empresa.Nombre}, @Telefono={empresa.Telefono}, @Email={empresa.Email}, @DireccionWeb={empresa.DireccionWeb}, @Logo={empresa.Logo}");

                    if (query > 0)
                    {
                        result.Message = "Se agrego la empresa correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar la empresa" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaGetAll '{empresa.Nombre}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empresa empresaa = new ML.Empresa();

                            empresaa.IdEmpresa = item.IdEmpresa;
                            empresaa.Nombre = item.Nombre;
                            empresaa.Telefono = item.Telefono;
                            empresaa.Email = item.Email;
                            empresaa.DireccionWeb = item.DireccionWeb;
                            empresaa.Logo = item.Logo;

                            result.Objects.Add(empresaa);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de empresas" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetByIdEF(int idEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var item = context.Empresas.FromSqlRaw($"EmpresaGetById '{idEmpresa}'").AsEnumerable().SingleOrDefault();

                    if (item != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = item.IdEmpresa;
                        empresa.Nombre = item.Nombre;
                        empresa.Telefono = item.Telefono;
                        empresa.Email = item.Email;
                        empresa.DireccionWeb = item.DireccionWeb;
                        empresa.Logo = item.Logo;

                        result.Objeto = empresa;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la empresa" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    //int query = context.Database.ExecuteSqlRaw($"EmpresaUpdate '{empresa.IdEmpresa}','{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}, '{empresa.Logo}'");
                    int query = context.Database.ExecuteSqlInterpolated($"EmpresaUpdate @Nombre={empresa.Nombre}, @Telefono={empresa.Telefono}, @Email={empresa.Email}, @DireccionWeb={empresa.DireccionWeb}, @Logo={empresa.Logo}, @IdEmpresa={empresa.IdEmpresa}");
                    if (query > 0)
                    {
                        result.Message = "Se actualizo la empresa correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar la empresa" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result DeleteEF(int idEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpresaDelete '{idEmpresa}'");

                    if (query > 0)
                    {
                        result.Message = "Se elimino la empresa correctamente";
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar la empresa" + result.Ex;

                throw;
            }
            return result;
        }
    }
}
