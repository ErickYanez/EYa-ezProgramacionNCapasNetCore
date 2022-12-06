using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Sexo.ToString()}', '{usuario.FechaNacimiento}', '{usuario.Password}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', '{usuario.UserName}', '{usuario.Email}', {usuario.Rol.IdRol},'{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', '{usuario.Direccion.Colonia.IdColonia}'");

                    if (query > 0)
                    {
                        result.Message = "Se inserto el usuario correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el usuario" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    usuario.Rol.IdRol = (byte)((usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol);
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.Rol.IdRol}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Usuario usuarioo = new ML.Usuario();

                            usuarioo.IdUsuario = item.IdUsuario;
                            usuarioo.Nombre = item.Nombre;
                            usuarioo.ApellidoPaterno = item.ApellidoPaterno;
                            usuarioo.ApellidoMaterno = item.ApellidoMaterno;
                            usuarioo.Sexo = char.Parse(item.Sexo.ToString().Trim());
                            usuarioo.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            usuarioo.Password = item.Password;
                            usuarioo.Telefono = item.Telefono;
                            usuarioo.Celular = item.Celular;
                            usuarioo.CURP = item.Curp;
                            usuarioo.UserName = item.UserName;
                            usuarioo.Email = item.Email;
                            usuarioo.Imagen = item.Imagen;
                            usuarioo.Status = item.Status.Value;

                            usuarioo.NombreCompleto = $"{item.Nombre},{item.ApellidoPaterno},{item.ApellidoMaterno}";

                            usuarioo.Rol = new ML.Rol();
                            usuarioo.Rol.IdRol = item.IdRol.Value;
                            usuarioo.Rol.Nombre = item.NombreRol;

                            usuarioo.Direccion = new ML.Direccion();
                            usuarioo.Direccion.IdDireccion = item.IdDireccion;
                            usuarioo.Direccion.Calle = item.Calle;
                            usuarioo.Direccion.NumeroInterior = item.NumeroInterior;
                            usuarioo.Direccion.NumeroExterior = item.NumeroExterior;

                            usuarioo.Direccion.Colonia = new ML.Colonia();
                            usuarioo.Direccion.Colonia.IdColonia = item.IdColonia;
                            usuarioo.Direccion.Colonia.Nombre = item.NombreColonia;
                            usuarioo.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                            usuarioo.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioo.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio;
                            usuarioo.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

                            usuarioo.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioo.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado;
                            usuarioo.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

                            usuarioo.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarioo.Direccion.Colonia.Municipio.Estado.Pais.IdPais = item.IdPais;
                            usuarioo.Direccion.Colonia.Municipio.Estado.Pais.Nombre = item.NombrePais;

                            result.Objects.Add(usuarioo);
                        }

                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de usuarios" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetByIdEF(byte idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var item = context.Usuarios.FromSqlRaw($"UsuarioGetById '{idUsuario}'").AsEnumerable().SingleOrDefault();

                    if (item != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = item.IdUsuario;
                        usuario.Nombre = item.Nombre;
                        usuario.ApellidoPaterno = item.ApellidoPaterno;
                        usuario.ApellidoMaterno = item.ApellidoMaterno;
                        usuario.Sexo = char.Parse(item.Sexo.ToString().Trim());
                        usuario.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        usuario.Password = item.Password;
                        usuario.Telefono = item.Telefono;
                        usuario.Celular = item.Celular;
                        usuario.CURP = item.Curp;
                        usuario.UserName = item.UserName;
                        usuario.Email = item.Email;
                        usuario.Imagen = item.Imagen;

                        usuario.NombreCompleto = $"{item.Nombre} {item.ApellidoPaterno} {item.ApellidoMaterno}";

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = item.IdRol.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = item.IdDireccion;
                        usuario.Direccion.Calle = item.Calle;
                        usuario.Direccion.NumeroInterior = item.NumeroInterior;
                        usuario.Direccion.NumeroExterior = item.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = item.IdColonia;
                        usuario.Direccion.Colonia.Nombre = item.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = item.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = item.NombrePais;

                        result.Objeto = usuario;
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de usuarios" + result.Ex;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.IdUsuario}','{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Sexo.ToString()}', '{usuario.FechaNacimiento}', '{usuario.Password}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', '{usuario.UserName}', '{usuario.Email}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', '{usuario.Direccion.Colonia.IdColonia}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo el usuario correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar el usuario" + result.Ex;
            }
            return result;
        }

        public static ML.Result DeleteEF(byte idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete '{idUsuario}'");

                    if (query > 0)
                    {
                        result.Message = "Se elimino el usuario correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el usuario" + result.Ex;
            }
            return result;
        }

        public static ML.Result ConvertirExcelADataTable(string connection)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connection))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                         OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow item in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.Nombre = item[0].ToString();
                                usuario.ApellidoPaterno = item[1].ToString();
                                usuario.ApellidoMaterno = item[2].ToString();
                                usuario.Sexo = usuario.Sexo = (item[3].ToString().IsNullOrEmpty() || item[3].ToString().Length > 1) ? '\0' : char.Parse(item[3].ToString().Trim());
                                usuario.FechaNacimiento = item[4].ToString();
                                usuario.Password = item[5].ToString();
                                usuario.Telefono = item[6].ToString();
                                usuario.Celular = item[7].ToString();
                                usuario.CURP = item[8].ToString();
                                usuario.UserName = item[9].ToString();
                                usuario.Email = item[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = byte.Parse(item[11].ToString());

                                usuario.Imagen = null;

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = item[12].ToString();
                                usuario.Direccion.NumeroInterior = item[13].ToString();
                                usuario.Direccion.NumeroExterior = item[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(item[15].ToString());

                                result.Objects.Add(usuario);
                            }
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el usuario" + result.Ex;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido paterno ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido materno ";
                    }
                    if (usuario.Sexo.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el sexo ";
                    }
                    if(usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la fecha de nacimiento ";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el password ";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono ";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el celular ";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar el CURP ";
                    }
                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el UserName ";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email ";
                    }
                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdRol ";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la Calle ";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresar el NumeroInterior ";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el NumeroExterior ";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdColonia ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                    result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el usuario" + result.Ex;
            }
            return result;
        }

        public static ML.Result ChangeStatus(byte idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus '{idUsuario}','{status}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo el status correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar el status" + result.Ex;
            }
            return result;
        }
    }
}
