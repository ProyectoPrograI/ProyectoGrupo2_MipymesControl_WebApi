using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2EF.Modelos;
using Grupo_2_Web_API.APIModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace Grupo_2_Web_API.Controllers
{
    [EnableCors("bibliotecaPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public  class CreditosController : ControllerBase
    {
        //Creamos una instacia del context para gestionar la base de datos.
        private ProyectoContext dataBase;

        public CreditosController(ProyectoContext dbContext)
        {
            this.dataBase = dbContext;
        }


        //GET /api/creditos/{id}  --Obtiene todos los creditos registrados para un id admin
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CreditoAPI>>> GetCreditos(int id)
        {
            var consultaCreditos = from e in dataBase.Creditos
                                   where e.IdAdmi == id
                                   select new CreditoAPI
                                   {
                                       IdCliente = e.IdCliente,
                                       IdAdmi = id,
                                       Nombre = e.Nombre,
                                       Apellido = e.Apellido,
                                       Deuda = e.Deuda
                                   };
            List<CreditoAPI> listaCreditos = await consultaCreditos.ToListAsync();

            return listaCreditos;
        }

        //POST /api/creditos  --Agrega un credito a la tabla creditos
        [HttpPost]
        public async Task<ActionResult<CreditoAPI>> AgregarCredito(CreditoAPI credito)
        {
            Credito nuevoCredito = new Credito();
            nuevoCredito.IdAdmi = credito.IdAdmi;
            nuevoCredito.Nombre = credito.Nombre;
            nuevoCredito.Apellido = credito.Apellido;
            nuevoCredito.Deuda = credito.Deuda;

            try
            {
                dataBase.Creditos.Add(nuevoCredito);
                int filasAfectadas = await dataBase.SaveChangesAsync();
                if (filasAfectadas > 0)
                    return CreatedAtAction("AgregarCredito", new { estado = nuevoCredito.IdCliente });
                else
                    return StatusCode(500);
            }
            catch (Exception e)
            {
                return Conflict(e);
            }
        }

        //PUT /api/creditos/{idCliente}
        [HttpPut("{idCliente}")]
        public async Task<ActionResult<CreditoAPI>> ActualizarCredito(int idCliente, CreditoAPI credito)
        {
            Credito encontrado = dataBase.Creditos.Find(idCliente);
            if (encontrado != null)
            {
                encontrado.Nombre = credito.Nombre;
                encontrado.Apellido = credito.Apellido;
                encontrado.Deuda = credito.Deuda;

                int filasAfectadas = await dataBase.SaveChangesAsync();
                if (filasAfectadas > 0)
                    return Ok(new { estado = "Modificado" });
                else
                    return StatusCode(500);
            }
            else
                return NotFound();
        }

        //Delete /api/creditos/{id}
        [HttpDelete("{IdCliente}")]
        public async Task<ActionResult<CreditoAPI>> BorrarCredito(int IdCliente)
        {
            //Borramos todos los movimientos pertenecientes a ese cliente
            var borrarMovimientos =
            from movimientos in dataBase.Movimientos
            where movimientos.IdCliente == IdCliente
            select movimientos;

            foreach (var element in borrarMovimientos)
            {
                dataBase.Movimientos.Remove(element);
            }

            try
            {
                await dataBase.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Borramos el cliente
            Credito encontrado = dataBase.Creditos.Find(IdCliente);
            if (encontrado != null)
            {
                dataBase.Creditos.Remove(encontrado);
                int filasAfectadas = await dataBase.SaveChangesAsync();
                if (filasAfectadas > 0)
                    return Ok(new { estado = "Eliminado" });
                else
                    return StatusCode(500);
            }
            else
                return NotFound();
        }
    }
}