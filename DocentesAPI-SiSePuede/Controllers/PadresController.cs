using DocentesAPI_SiSePuede.Models;
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
        public IActionResult GetPadre(int idAlumno)
        {
            var idtutores = alumnoturorepos.GetAll().Where(x => x.IdAlumno == idAlumno).Select(x => x.IdTutor);
            var tutores = padrerepos.GetAll().Where(x => idtutores.Contains(x.Id));
            return Ok(tutores);
        }

        [HttpPost("/AgregarPadre")]
        public IActionResult AgregarPadre(Tutor tutor)
        {
            if (tutor == null)
                return BadRequest();
            padrerepos.Insert(tutor);
            return Ok();
        }

        [HttpPut("/EditarPadre")]
        public IActionResult EditarPadre(Tutor tutor)
        {
            if (tutor == null)
                return BadRequest();
            var a = padrerepos.GetById(tutor.Id);
            if (a == null)
                return NotFound();
            else
            {
                a.Nombre = tutor.Nombre;
                a.Direccion = tutor.Direccion;
                a.Telefono = tutor.Telefono;
                a.Celular = tutor.Celular;
                a.Email = tutor.Email;
                a.Idusuario = tutor.Idusuario;
                a.Ocupacion = tutor.Ocupacion;
            }
            padrerepos.Update(a);
            return Ok();
        }

        [HttpPost("/EliminarPadre")]
        public IActionResult EliminarPadre(int id, int idAlumno)
        {
            if (id == 0)
                return BadRequest();
            var tutor = alumnoturorepos.GetAll().Where(x => x.IdTutor == id && x.IdAlumno == idAlumno).FirstOrDefault();
            if (tutor == null)
                return NotFound();
            alumnoturorepos.Delete(tutor);
            return Ok();
        }
    }
}
