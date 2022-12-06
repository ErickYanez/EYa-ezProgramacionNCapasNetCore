using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}','{empleado.Empresa.IdEmpresa}'");

                    if (query > 0)
                    {
                        result.Message = "Se agrego el empleado correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar a el empleado" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.NumeroEmpleado = item.NumeroEmpleado;
                            empleado.RFC = item.Rfc;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.Email = item.Email;
                            empleado.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            empleado.NSS = item.Nss;
                            empleado.FechaIngreso = item.FechaIngreso.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            empleado.Foto = item.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = item.IdEmpresa.Value;
                            empleado.Empresa.Nombre = item.NombreEmpresa;

                            result.Objects.Add(empleado);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de empleados" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var item = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().SingleOrDefault();

                    if (item != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumeroEmpleado = item.NumeroEmpleado;
                        empleado.RFC = item.Rfc;
                        empleado.Nombre = item.Nombre;
                        empleado.ApellidoPaterno = item.ApellidoPaterno;
                        empleado.ApellidoMaterno = item.ApellidoMaterno;
                        empleado.Email = item.Email;
                        empleado.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        empleado.NSS = item.Nss;
                        empleado.FechaIngreso = item.FechaIngreso.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        empleado.Foto = item.Foto;

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = item.IdEmpresa.Value;
                        empleado.Empresa.Nombre = item.NombreEmpresa;

                        result.Objeto = empleado;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el empleado" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}','{empleado.Empresa.IdEmpresa}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo el empleado correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar a el empleado" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");

                    if (query > 0)
                    {
                        result.Message = "Se elimino el empleado correctamente";
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar a el empleado" + result.Ex;

                throw;
            }
            return result;
        }
    }
}
