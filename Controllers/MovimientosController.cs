using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2EF.Modelos;
using Grupo_2_Web_API.APIModels;
using Microsoft.EntityFrameworkCore;

namespace Grupo_2_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        //Creamos una instacia del context para gestionar la base de datos.
        private ProyectoContext dataBase;
        public MovimientosController(ProyectoContext dbContext)
        {
            this.dataBase = dbContext;
        }

        //GET /api/movimientos/{id}  --Obtiene todos los movientos registrados para un id admin
        [HttpGet("{idAdministrador}")]
        public async Task<ActionResult<List<MovimientoAPI>>> GetMovimientos(int idAdministrador)
        {
            var consultaMovimientos = from s in dataBase.Movimientos
                                      where s.IdClienteNavigation.IdAdmi == idAdministrador
                                      select new MovimientoAPI
                                      {
                                          IdMovimiento = s.Id,
                                          IdAdministrador = s.IdClienteNavigation.IdAdmi,
                                          IdCliente = s.IdClienteNavigation.IdCliente,
                                          Nombre = s.IdClienteNavigation.Nombre,
                                          Apellido = s.IdClienteNavigation.Apellido,
                                          Deuda = s.IdClienteNavigation.Deuda,
                                          Tipo = s.Tipo,
                                          Fecha = s.Fecha
                                      };

            List<MovimientoAPI> listaMovimientos = await consultaMovimientos.ToListAsync();
            return listaMovimientos;
        }

        //POST /api/movimientos  --Agrega un movimiento a la tabla movimientos
        [HttpPost]
        public ActionResult<MovimientoPostAPI> AgregarMovimiento(MovimientoPostAPI movimiento)
        {
            Movimiento nuevoMovimiento = new Movimiento();
            nuevoMovimiento.IdCliente = movimiento.IdCliente;
            nuevoMovimiento.Tipo = movimiento.Tipo;
            nuevoMovimiento.Fecha = DateTime.Now;

            try
            {
                dataBase.Movimientos.Add(nuevoMovimiento);
                int filasAfectadas = dataBase.SaveChanges();
                if (filasAfectadas > 0)
                    return CreatedAtAction("AgregarMovimiento", new { estado = "Agregado" });
                else
                    return StatusCode(500);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
        }
    }
}