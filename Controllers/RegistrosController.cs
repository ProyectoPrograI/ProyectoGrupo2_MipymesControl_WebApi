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
    public class RegistrosController : ControllerBase
    {
        private ProyectoContext dataBase;
        public RegistrosController(ProyectoContext dbContext)
        {
            this.dataBase = dbContext;
        }

        //POST /api/registros  --Para el login
        [HttpPost]
        public async Task<ActionResult<RegistroAPI>> IniciarSesion(RegistroCortoAPI registro)
        {
            /*
             Ejemplo de body para el post: (Solo es necesario pasar el correo y contrasenia)
            
                {
                    "Correo": "NSanchez@gmail.com",
                    "Contrasenia": "Arroz" 
                }
        
             */


            //Buscamos el id admin donde el correo y contrasenia coincidan con lo ingresado.
            var buscar =
            from s in dataBase.Registros
            where s.Correo.Equals(registro.Correo) && s.Contrasenia.Equals(registro.Contrasenia)
            select new RegistroAPI
            {
                IdAdmi = s.IdAdmi,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Correo = s.Correo,
                Celular = s.Celular,
                Contrasenia = ""
            };

            //Retornamos el registro que coincidio con el correo y contrasenia especificada.
            try
            {
                RegistroAPI existe = await buscar.SingleAsync();
                if (!existe.Correo.Equals("") && existe.Correo != null)
                {
                    existe.Contrasenia = "";
                    return Ok(existe);
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