using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int idColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio '{idColonia}'").AsEnumerable().ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = item.IdColonia;
                            colonia.Nombre = item.Nombre;

                            result.Objects.Add(colonia);
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
