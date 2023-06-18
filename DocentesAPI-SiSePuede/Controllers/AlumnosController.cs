using DocentesAPI_SiSePuede.Models;
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
        [HttpGet("AlumnoGrupo/{id}")]
        public IActionResult AlumnoGrupo(int id)
        {
            var alumnos = alumnorepo.GetAll().Where(x => x.IdGrupo == id);
            return Ok(alumnos);
        }

        [HttpPost("AgregarAlumno")]
        public IActionResult AgregarAlumno(Alumno alumno)
        {
            if (alumno == null)
                return BadRequest();
            alumnorepo.Insert(alumno);
            return Ok();
        }

        [HttpPut("EditarAlumno")]
        public IActionResult EditarAlumno(Alumno alumno)
        {
            if (alumno == null)
                return BadRequest();
            var a = alumnorepo.GetById(alumno.Id);
            if (a == null)
                return NotFound();
            else
            {
                a.Matricula = alumno.Matricula;
                a.Nombre = alumno.Nombre;
                a.Direccion = alumno.Direccion;
                a.Curp = alumno.Curp;
                a.IdGrupo = alumno.IdGrupo;
                a.Alergico = alumno.Alergico;
                a.Peso = alumno.Peso;
                a.Estatura = alumno.Estatura;
                a.FechaNacimiento = alumno.FechaNacimiento;
                a.Edad = alumno.Edad;
            }
            alumnorepo.Update(a);
            return Ok();
        }

        [HttpPost("EliminarAlumno")]
        public IActionResult EliminarAlumno(Alumno a)
        {
            var alumno = alumnorepo.GetAll().Where(x=>x.Id==a.Id).FirstOrDefault();
            if (alumno == null)
                return NotFound();
            alumnorepo.Delete(alumno);
            return Ok();
        }
        #endregion

        #region ShowDeLosGrupos
        [HttpGet("Docente/{id}")]
        public IActionResult GetDocente(int id)
        {
            Grupo grupos = new();
            var docenteGrupos = grupodocenterepo.GetAll().Where(x => x.IdDocente == id).FirstOrDefault();
            if (docenteGrupos != null)
                grupos = gruporepo.GetById(docenteGrupos.IdGrupo);
            return Ok(grupos);
        }
        #endregion
    }
}
