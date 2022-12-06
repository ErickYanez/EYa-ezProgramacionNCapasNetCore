using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EyañezProgramacionNcapasContext context = new DL.EyañezProgramacionNcapasContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw("DependienteTipoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.DependienteTipo dependientetipo = new ML.DependienteTipo();

                            dependientetipo.IdDependienteTipo = item.IdDependienteTipo;
                            dependientetipo.Nombre = item.Nombre;

                            result.Objects.Add(dependientetipo);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista de dependiente tipo" + result.Ex;

                throw;
            }
            return result;
        }
    }
}
