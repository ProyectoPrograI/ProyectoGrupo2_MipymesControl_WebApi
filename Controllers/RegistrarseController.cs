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
    public class RegistrarseController : ControllerBase
    {
        private ProyectoContext dataBase;
        public RegistrarseController(ProyectoContext dbContext)
        {
            this.dataBase = dbContext;
        }

        //POST /api/registros  --Para registrarse

        /*
         Ejemplo de body:
        {
            "IdAdmi": 0,
            "Nombre": "Ernesto",
            "Apellido": "Monjes",
            "Correo": "ernesto.monjes@gmail.com",
            "Celular": "94470138",
            "Contrasenia": "zapato" 
        }
         */
        [HttpPost]
        public async Task<ActionResult<RegistroAPI>> Registrarse(RegistroAPI registro)
        {
            Registro nuevoRegistro = new Registro();
            nuevoRegistro.Nombre = registro.Nombre;
            nuevoRegistro.Apellido = registro.Apellido;
            nuevoRegistro.Correo = registro.Correo;
            nuevoRegistro.Celular = registro.Celular;
            nuevoRegistro.Contrasenia = registro.Contrasenia;

            try
            {
                dataBase.Registros.Add(nuevoRegistro);
                if (await dataBase.SaveChangesAsync() > 0)
                {
                    registro.IdAdmi = nuevoRegistro.IdAdmi;
                    registro.Contrasenia = "";
                    return Ok(new { usuario = registro });
                }
                else
                    return StatusCode(400);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}