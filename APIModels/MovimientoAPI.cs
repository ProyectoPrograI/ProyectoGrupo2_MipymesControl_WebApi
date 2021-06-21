using System;
namespace Grupo_2_Web_API.APIModels
{
    public class MovimientoAPI
    {
        public int IdMovimiento { get; set; }
        public int IdAdministrador { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Deuda { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
