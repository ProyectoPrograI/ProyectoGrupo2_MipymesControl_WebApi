using System;
namespace Grupo_2_Web_API.APIModels
{
    public class RegistroAPI
    {
        public int IdAdmi { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Contrasenia { get; set; }
    }
}
