using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public byte IdRol { get; set; }

        [StringLength(50)]
        public string? Nombre { get; set; }
        public List<Object>? Roles { get; set; }
    }
}
