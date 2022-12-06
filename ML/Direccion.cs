using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }

        //[Required]
        [StringLength(50)]
        public string? Calle { get; set; }

        //[Required]
        [Display(Name = "Numero Interior")]
        [StringLength(20)]
        public string? NumeroInterior { get; set; }

        //[Required]
        [Display(Name = "Numero Exterior")]
        [StringLength(20)]
        public string? NumeroExterior { get; set; }
        public ML.Colonia? Colonia { get; set; }
        public List<Object>? Direcciones { get; set; }
    }
}
