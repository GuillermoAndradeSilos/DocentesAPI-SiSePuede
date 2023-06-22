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
        Repository<Asistencias> Repositorio;
        public AsistenciaController(Sistem21PrimariaContext context)
        {
            this.context = context;
            Repositorio = new Repository<Asistencias>(context);
        }

        [HttpGet("/ObtenerAsistencias")]
        public IActionResult GetAll()
        {
            var asistencias = Repositorio.GetAll();
            return Ok(asistencias);
        }

        [HttpGet("/ObtenerAsistencias/{id}")]
        public IActionResult GetbyID(int id)
        {
            var asistencias = Repositorio.GetAll().Where(x => x.IdAlumno == id);
            if (asistencias == null)
                return Conflict("No existe el alumno");
            return Ok(asistencias);
        }
        [HttpPost("/AgregarAsistencia")]
        public IActionResult Post(Asistencias a)
        {
            var anterior = Repositorio.GetById(a.Id);
            if (anterior != null)
            {
                return Conflict("Ya esta registrada la asistencia");
            }
            else
            {
                if (a.Fecha < DateOnly.FromDateTime(DateTime.Now))
                {
                    return Conflict("No puedes registrar asistencias de dias anteriores al de hoy");
                }
                if (a.IdAlumno == 0)
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
        [HttpPut("/EditarAsistencia")]
        public IActionResult Put(Asistencias a)
        {
            var anterior = Repositorio.GetById(a.Id);
            if (anterior == null)
            {
                return NotFound("No existe la asistencia que desea editar");
            }
            if (a.Fecha < DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("No puedes colocar una fecha anterior a la fecha actual");
            }
            if (a.IdAlumno == 0)
            {
                return Conflict("No puedes editar la asistencia de un alumno que no existe");
            }
            Repositorio.Update(a);
            return Ok();
        }

    }
}
