using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Proyecto2EF.Modelos
{
    public partial class Credito
    {
        public Credito()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        [Key]
        public int IdCliente { get; set; }
        [Column("Id_Admi")]
        public int IdAdmi { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Deuda { get; set; }

        [ForeignKey(nameof(IdAdmi))]
        [InverseProperty(nameof(Registro.Creditos))]
        public virtual Registro IdAdmiNavigation { get; set; }
        [InverseProperty(nameof(Movimiento.IdClienteNavigation))]
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
