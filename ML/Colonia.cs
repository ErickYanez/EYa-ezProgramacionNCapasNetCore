using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        public int IdColonia { get; set; }

        [StringLength(50)]
        public string? Nombre { get; set; }

        [StringLength(10)]
        public string? CodigoPostal { get; set; }
        public ML.Municipio? Municipio { get; set; }
        public List<Object>? Colonias { get; set; }
    }
}
