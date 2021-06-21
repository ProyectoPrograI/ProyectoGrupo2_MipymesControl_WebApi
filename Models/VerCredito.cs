using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Proyecto2EF.Modelos
{
    [Keyless]
    public partial class VerCredito
    {
        public int IdAdmi { get; set; }
        public int IdCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Deuda { get; set; }
    }
}
