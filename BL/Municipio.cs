using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int idMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado '{idMunicipio}'").AsEnumerable().ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = item.IdMunicipio;
                            municipio.Nombre = item.Nombre;

                            result.Objects.Add(municipio);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el estado" + result.Ex;

                throw;
            }
            return result;
        }
    }
}
