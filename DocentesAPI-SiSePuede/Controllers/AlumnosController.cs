using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Models.DTOs;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocentesAPI_SiSePuede.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        /// <summary>
        /// Aqui esta el crud en crudo bdtsss
        /// Solo chequen las rutas para los metodos y ver que pidan pa que traigan la info y asi aja si
        /// NO MOVER o me esponjo
        /// </summary>
        private readonly Sistem21PrimariaContext cx;
        private readonly Repository<Alumno> alumnorepo;
        private readonly Repository<Grupo> gruporepo;
        private readonly Repository<DocenteGrupo> grupodocenterepo;
        public AlumnosController(Sistem21PrimariaContext cx)
        {
            this.cx = cx;
            alumnorepo = new Repository<Alumno>(cx);
            gruporepo = new Repository<Grupo>(cx);
            grupodocenterepo = new Repository<DocenteGrupo>(cx);
        }
        #region CRUD-ALUMNO
        [HttpGet("/AlumnoGrupo/{id}")]
        public IActionResult AlumnoGrupo(int id)
        {
            var alumnos = alumnorepo.GetAll().Where(x => x.IdGrupo == id).ToList();
            return Ok(alumnos);
        }

        [HttpPost("/AgregarAlumno")]
        public IActionResult AgregarAlumno(AlumnoDTO alumno)
        {
            if (alumno == null)
                return BadRequest();
            alumnorepo.Insert(alumno.Alumno);
            return Ok();
        }

        [HttpPost("/EditarAlumno")]
        public IActionResult EditarAlumno(AlumnoDTO alumno)
        {
            if (alumno == null)
                return BadRequest();
            var alumnoeditar = alumnorepo.GetById(alumno.Alumno.Id);
            if (alumnoeditar == null)
                return NotFound();
            else
            {
                alumnoeditar.Matricula = alumno.Alumno.Matricula;
                alumnoeditar.Nombre = alumno.Alumno.Nombre;
                alumnoeditar.Direccion = alumno.Alumno.Direccion;
                alumnoeditar.Curp = alumno.Alumno.Curp;
                alumnoeditar.IdGrupo = alumno.Alumno.IdGrupo;
                alumnoeditar.Alergico = alumno.Alumno.Alergico;
                alumnoeditar.Peso = alumno.Alumno.Peso;
                alumnoeditar.Estatura = alumno.Alumno.Estatura;
                alumnoeditar.FechaNacimiento = alumno.Alumno.FechaNacimiento;
                alumnoeditar.Edad = alumno.Alumno.Edad;
            }
            alumnorepo.Update(alumnoeditar);
            return Ok();
        }

        [HttpPost("/EliminarAlumno")]
        public IActionResult EliminarAlumno(AlumnoDTO alumno)
        {
            var alumnoeliminar = alumnorepo.GetAll().Where(x => x.Id == alumno.Alumno.Id).FirstOrDefault();
            if (alumnoeliminar == null)
                return NotFound();
            alumnorepo.Delete(alumnoeliminar);
            return Ok();
        }
        #endregion

        #region ShowDeLosGrupos
        [HttpGet("/GrupoDocente/{id}")]
        public IActionResult GetGrupoDocente(int id)
        {
            Grupo grupos = new();
            var docenteGrupos = grupodocenterepo.GetAll().Where(x => x.IdDocente == id).FirstOrDefault();
            if (docenteGrupos != null)
                grupos = gruporepo.GetById(docenteGrupos.IdGrupo);
            grupos.DocenteGrupo = null;
            return Ok(grupos);
        }
        #endregion
    }
}
