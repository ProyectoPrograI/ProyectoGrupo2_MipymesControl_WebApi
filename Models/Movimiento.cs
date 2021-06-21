using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Proyecto2EF.Modelos
{
    public partial class Movimiento
    {
        [Key]
        public int Id { get; set; }
        [Column("Id_Cliente")]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [InverseProperty(nameof(Credito.Movimientos))]
        public virtual Credito IdClienteNavigation { get; set; }
    }
}
