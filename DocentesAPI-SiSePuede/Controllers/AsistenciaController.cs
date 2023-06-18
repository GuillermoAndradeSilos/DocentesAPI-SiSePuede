using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocentesAPI_SiSePuede.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private Sistem21PrimariaContext context;
        Repository<Asistencia> Repositorio;
        public AsistenciaController(Sistem21PrimariaContext context)
        {
            this.context = context;
            Repositorio = new Repository<Asistencia>(context);
        }

        [HttpGet("/ObtenerAsistencias")]
        public IActionResult GetAll()
        {
            var asistencias = Repositorio.GetAll();
            return Ok(asistencias);
        }

        [HttpGet("/ObtenerAsistencias/{id:int}")]
        public IActionResult GetbyID(int id)
        {
            var asistencias = Repositorio.GetById(id);
            if(asistencias == null)
            {
                return Conflict("No existe el alumno");
            }
            return Ok();
        }
        [HttpPost("/AgregarAsistencia")]
        public IActionResult Post(Asistencia a)
        {
            var anterior = Repositorio.GetById(a.Id);
            if (anterior != null)
            {
                return Conflict("Ya esta registrada la asistencia");
            }
            else
            {
                if (a.Fecha<DateOnly.FromDateTime(DateTime.Now))
                {
                    return Conflict("No puedes registrar asistencias de dias anteriores al de hoy");
                }
                if (a.IdAlumno==0)
                {
                    return Conflict("No puedes registrar asistencias a un alumno que no existe");
                }
                else
                {
                    Repositorio.Insert(a);
                    return Ok("Se ah registrado correctamente la asistencia");
                }
            }
        }
        [HttpDelete("/EliminarAsistencia")]
        public IActionResult Delete(Asistencia a)
        {
            var anterior = Repositorio.GetById(a.Id);
            if (anterior == null)
            {
                return BadRequest("La eliminacion de la asistencia fracaso");
            }
            else
            {
                if (a.IdAlumno==0)
                {
                    return Conflict("No puedes eliminar asistencias a un alumno que no existe");
                }
                else
                {
                    Repositorio.Delete(a);
                    return Ok("Se ah eliminado correctamente la asistencia");
                }
            }
        }
        [HttpPut("/EditarAsistencia")]
        public IActionResult Put(Asistencia a)
        {
            var anterior = Repositorio.GetById(a.Id);
            if (anterior!=null)
            {
                return NotFound("No existe la asistencia que desea editar");
            }
            if (a.Fecha<DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("No puedes colocar una fecha anterior a la fecha actual");
            }
            if (a.IdAlumno==0)
            {
                return Conflict("No puedes editar la asistencia de un alumno que no existe");
            }
            return Ok();
        }

    }
}
