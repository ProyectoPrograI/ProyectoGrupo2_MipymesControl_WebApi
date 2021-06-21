using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Proyecto2EF.Modelos
{
    public partial class Registro
    {
        public Registro()
        {
            Creditos = new HashSet<Credito>();
        }

        [Key]
        public int IdAdmi { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
        [StringLength(20)]
        public string Celular { get; set; }
        [Required]
        [StringLength(30)]
        public string Contrasenia { get; set; }

        [InverseProperty(nameof(Credito.IdAdmiNavigation))]
        public virtual ICollection<Credito> Creditos { get; set; }
    }
}
