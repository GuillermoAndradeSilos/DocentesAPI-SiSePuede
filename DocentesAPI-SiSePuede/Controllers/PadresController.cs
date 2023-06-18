using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Models.DTOs;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocentesAPI_SiSePuede.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PadresController : ControllerBase
    {
        /// <summary>
        /// Este es el de los tutores pero lo pongo como padres porque un loco
        /// chisquiado me dijo que creia que eran tutores de maestros, mich si ves esto, wtf?
        /// NO MOVER (creo que no deberia ser necesario moverle para nada a esto)
        /// </summary>
        private readonly Sistem21PrimariaContext cx;
        private readonly Repository<Tutor> padrerepos;
        private readonly Repository<AlumnoTutor> alumnoturorepos;
        public PadresController(Sistem21PrimariaContext cx)
        {
            this.cx = cx;
            padrerepos = new Repository<Tutor>(this.cx);
            alumnoturorepos = new Repository<AlumnoTutor>(this.cx);
        }
        [HttpGet("/GetPadre")]
        public IActionResult GetPadre(TutorDTO tutor)
        {
            var idtutores = alumnoturorepos.GetAll().Where(x => x.IdAlumno == tutor.Alumno).Select(x => x.IdTutor);
            var tutores = padrerepos.GetAll().Where(x => idtutores.Contains(x.Id));
            return Ok(tutores);
        }

        [HttpPost("/AgregarPadre")]
        public IActionResult AgregarPadre(TutorDTO tutor)
        {
            if (tutor == null)
                return BadRequest();
            padrerepos.Insert(tutor.Tutor);
            return Ok();
        }

        [HttpPut("/EditarPadre")]
        public IActionResult EditarPadre(TutorDTO tutor)
        {
            if (tutor == null)
                return BadRequest();
            var a = padrerepos.GetById(tutor.Tutor.Id);
            if (a == null)
                return NotFound();
            else
            {
                a.Nombre = tutor.Tutor.Nombre;
                a.Direccion = tutor.Tutor.Direccion;
                a.Telefono = tutor.Tutor.Telefono;
                a.Celular = tutor.Tutor.Celular;
                a.Email = tutor.Tutor.Email;
                a.Idusuario = tutor.Tutor.Idusuario;
                a.Ocupacion = tutor.Tutor.Ocupacion;
            }
            padrerepos.Update(a);
            return Ok();
        }

        [HttpPost("/EliminarPadre")]
        public IActionResult EliminarPadre(TutorDTO tutor)
        {
            if (tutor.Tutor != null && tutor.Alumno != 0)
                return BadRequest();
            var padre = alumnoturorepos.GetAll().Where(x => x.IdTutor == tutor.Tutor.Id && x.IdAlumno == tutor.Alumno).FirstOrDefault();
            if (padre == null)
                return NotFound();
            alumnoturorepos.Delete(padre);
            return Ok();
        }
    }
}
