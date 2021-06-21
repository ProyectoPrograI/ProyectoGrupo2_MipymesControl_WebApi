using System;
namespace Grupo_2_Web_API.APIModels
{
    public class CreditoAPI
    {
        public int IdCliente { get; set; }
        public int IdAdmi { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Deuda { get; set; }
    }
}
