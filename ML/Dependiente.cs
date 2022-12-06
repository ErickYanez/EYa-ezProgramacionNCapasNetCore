using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        public int IdDependiente { get; set; }

        public ML.Empleado Empleado { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? EstadoCivil { get; set; }

        public char Genero { get; set; }

        public string? Telefono { get; set; }

        public string? Rfc { get; set; }

        public ML.DependienteTipo DependienteTipo { get; set; }

        public List<Object> Dependientes { get; set; }
    }
}
