using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.Rfc}','{dependiente.DependienteTipo.IdDependienteTipo}'");

                    if (query > 0)
                    {
                        result.Message = "Se agrego el dependiente correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el dependiente" + result.Ex;
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
                    var query = context.Dependientes.FromSqlRaw("DependienteGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = item.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                            dependiente.Empleado.Nombre = item.NombreEmpleado;
                            dependiente.Nombre = item.Nombre;
                            dependiente.ApellidoPaterno = item.ApellidoPaterno;
                            dependiente.ApellidoMaterno = item.ApellidoMaterno;
                            dependiente.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dependiente.EstadoCivil = item.EstadoCivil;
                            dependiente.Genero = char.Parse(item.Genero.ToString().Trim());
                            dependiente.Telefono = item.Telefono;
                            dependiente.Rfc = item.Rfc;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = item.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = item.NombreDespendienteTipo;

                            result.Objects.Add(dependiente);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de dependientes" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado '{NumeroEmpleado}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.IdDependiente = item.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                            dependiente.Empleado.Nombre = item.NombreEmpleado;
                            dependiente.Nombre = item.Nombre;
                            dependiente.ApellidoPaterno = item.ApellidoPaterno;
                            dependiente.ApellidoMaterno = item.ApellidoMaterno;
                            dependiente.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dependiente.EstadoCivil = item.EstadoCivil;
                            dependiente.Genero = char.Parse(item.Genero.ToString().Trim());
                            dependiente.Telefono = item.Telefono;
                            dependiente.Rfc = item.Rfc;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = item.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = item.NombreDespendienteTipo;

                            result.Objects.Add(dependiente);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los dependientes" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result GetById(int idDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var item = context.Dependientes.FromSqlRaw($"DependienteGetById '{idDependiente}'").AsEnumerable().SingleOrDefault();

                    if (item != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();

                        dependiente.IdDependiente = item.IdDependiente;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                        dependiente.Empleado.Nombre = item.NombreEmpleado;
                        dependiente.Nombre = item.Nombre;
                        dependiente.ApellidoPaterno = item.ApellidoPaterno;
                        dependiente.ApellidoMaterno = item.ApellidoMaterno;
                        dependiente.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dependiente.EstadoCivil = item.EstadoCivil;
                        dependiente.Genero = char.Parse(item.Genero.ToString().Trim());
                        dependiente.Telefono = item.Telefono;
                        dependiente.Rfc = item.Rfc;

                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo = item.IdDependienteTipo.Value;
                        dependiente.DependienteTipo.Nombre = item.NombreDespendienteTipo;

                        result.Objeto = dependiente;
                    }                  
                    
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los dependientes" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteUpdate '{dependiente.IdDependiente}','{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.Rfc}','{dependiente.DependienteTipo.IdDependienteTipo}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo el dependiente correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar el dependiente" + result.Ex;

                throw;
            }
            return result;
        }

        public static ML.Result Delete(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteDelete '{dependiente.IdDependiente}'");

                    if (query > 0)
                    {
                        result.Message = "Se elimino el dependiente correctamente";
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el dependiente" + result.Ex;

                throw;
            }
            return result;
        }
    }
}
