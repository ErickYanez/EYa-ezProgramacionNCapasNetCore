using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public int IdMunicipio { get; set; }

        [StringLength(50)]
        public string? Nombre { get; set; }
        public ML.Estado? Estado { get; set; }
        public List<Object>? Municipios { get; set; }
    }
}
