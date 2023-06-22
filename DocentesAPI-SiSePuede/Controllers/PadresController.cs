using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Models.DTOs;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly Repository<Alumno> alumnorepos;
        private readonly Repository<Usuario> usuariorepos;

        public PadresController(Sistem21PrimariaContext cx)
        {
            this.cx = cx;
            padrerepos = new Repository<Tutor>(this.cx);
            alumnoturorepos = new Repository<AlumnoTutor>(this.cx);
            alumnorepos = new Repository<Alumno>(cx);
            usuariorepos = new Repository<Usuario>(cx);
        }
        [HttpGet("/GetPadre")]
        public IActionResult GetPadre()
        {
            var tutores = padrerepos.GetAll().Select(x => new Tutor()
            {
                Telefono = x.Telefono,
                Nombre = x.Nombre,
                Direccion = x.Direccion,
                Id = x.Id
            }).ToList();
            return Ok(tutores);
        }
        [HttpGet("/GetPadre/{id}")]
        public IActionResult GetPadre(int id)
        {
            var tutores = alumnoturorepos.GetAll().Where(x => x.IdAlumnoNavigation.IdGrupo == id).Select(x => new Tutor()
            {
                Telefono = x.IdTutorNavigation.Telefono,
                Nombre = x.IdTutorNavigation.Nombre,
                Direccion = x.IdTutorNavigation.Direccion,
                Id = x.Id
            }).ToList();
            return Ok(tutores);
        }
        [HttpPost("/AgregarPadre")]
        public IActionResult AgregarPadre(TutorDTO tutor)
        {
            if (tutor == null)
                return BadRequest();
            tutor.Usuario.Rol = 3;
            usuariorepos.Insert(tutor.Usuario);
            tutor.Tutor.Idusuario = usuariorepos.GetAll().Where(x => x.Usuario1 == tutor.Tutor.Nombre).Select(x => x.Id).FirstOrDefault();
            padrerepos.Insert(tutor.Tutor);
            return Ok();
        }

        [HttpPost("/EditarPadre")]
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
                var aja = usuariorepos.GetAll().Where(x => x.Usuario1 == a.Nombre).Select(x => x.Id).FirstOrDefault();
                a.Idusuario = aja;
            }
            padrerepos.Update(a);
            var b = alumnoturorepos.GetAll().Where(x => x.IdAlumnoNavigation.Curp == tutor.curp).FirstOrDefault();
            if (b == null)
            {
                var c = alumnorepos.GetAll().Where(x => x.Curp == tutor.curp).FirstOrDefault();
                alumnoturorepos.Insert(new AlumnoTutor() { IdAlumno = c.Id, IdTutor = tutor.Tutor.Id });
            }
            return Ok();
        }

        [HttpPost("/EliminarPadre")]
        public IActionResult EliminarPadre(TutorDTO tutor)
        {
            if (tutor.Tutor == null)
                return BadRequest();
            var aja = alumnoturorepos.GetAll().Where(x => x.IdTutorNavigation.Nombre == tutor.Tutor.Nombre).Select(x => x.IdAlumno).FirstOrDefault();
            var padre = alumnoturorepos.GetAll().Where(x => x.IdTutor == tutor.Tutor.Id).FirstOrDefault();
            if (padre == null)
                return NotFound();
            alumnoturorepos.Delete(padre);
            return Ok();
        }
    }
}
