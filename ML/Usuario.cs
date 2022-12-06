using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ML
{
    public class Usuario
    {
        public byte IdUsuario { get; set; }

        //[Required]
        //[StringLength(50)]
        public string? Nombre { get; set; }

        //[Required]
        //[StringLength(50)]
        [Display(Name = "Apellido Paterno")]
        public string? ApellidoPaterno { get; set; }

        //[StringLength(50)]
        [Display(Name = "Apellido Materno")]
        public string? ApellidoMaterno { get; set; }

        //[Required]
        public char Sexo { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public string? FechaNacimiento { get; set; }

        //[Required]
        public string? Password { get; set; }

        //[Required]
        public string? Telefono { get; set; }

        //[Required]
        public string? Celular { get; set; }

        //[Required]
        //[StringLength(18)]
        public string? CURP { get; set; }

        //[Required]
        public string? UserName { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public string? Email { get; set; }

        public string? Imagen { get; set; }
        public bool Status { get; set; }
        public ML.Rol? Rol { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public List<Object>? Usuarios { get; set; }
        public string? NombreCompleto { get; set; }
    }
}