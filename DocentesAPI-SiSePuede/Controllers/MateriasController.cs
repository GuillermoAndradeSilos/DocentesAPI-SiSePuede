using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Models.DTOs;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocentesAPI_SiSePuede.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        /// <summary>
        /// Este controlador sera unicamente para las materias y las calificaciones
        /// por ende aqui tambien deberia de ir lo del comentario del docente (quizas, idk bro im sick en plan holy sick)
        /// REPITO UNICAMENTE MATERIAS Y CALIFICACIONES
        /// NO AUTORIZO ; SI ME LE MUEVEN ME MUERO Y ME LOS LLEVO A USTEDES DE PASO
        /// </summary>
        private readonly Sistem21PrimariaContext cx;
        private readonly Repository<Asignatura> materiasrepo;
        private readonly Repository<Calificacion> reposCalificacion;
        private readonly Repository<DocenteAlumno> docenterepo;
        public MateriasController(Sistem21PrimariaContext cx)
        {
            this.cx = cx;
            materiasrepo = new Repository<Asignatura>(cx);
            reposCalificacion = new Repository<Calificacion>(cx);
            docenterepo = new Repository<DocenteAlumno>(cx);
        }
        //Eso nada que agregar
        [HttpGet("/ObtenerMaterias")]
        public IActionResult ObtenerMaterias()
        {
            var asignaturas = materiasrepo.GetAll().Where(x => x.TipoAsignatura == 1);
            return Ok(asignaturas);
        }
        [HttpGet("/ObtenerComentario/{id}")]
        public IActionResult ObtenerComentario(int id)
        {
            var comentario = docenterepo.GetAll().Where(x => x.IdAlumno == id).Select(x => x.ComentarioDocente).FirstOrDefault();
            return Ok(comentario);
        }
        //Lo demas de abajo es lo de las calificaciones (ya me dio pereza poner region, mejor bajenle la temperatura a la region, ayuda no tengo minisplit :c)
        [HttpPost("/ObtenerCalificaciones")]
        public IActionResult ObtenerCalificaciones(CalificacionDTO calif)
        {
            var calificaciones = reposCalificacion.GetAll().Where(x => x.IdAlumno == calif.Alumno && x.IdAsignatura == calif.Asignatura);
            return Ok(calificaciones);
        }
        //Este show sera para hacer el reporte, dios quiera que funcione
        [HttpGet("/GenerarReporte/{idGrupo}/{idDocente}")]
        //"ehh ehh pero no se esta generando el reporte aqui solo est...", SHHHHH SHHH, shh un sabio una vez dijo string.Isnullorempy
        public IActionResult GenerarReporte(int idGrupo, int idDocente)
        {
            List<ReporteDTO> alumnosreporbados = new();
            //a la linea de abajo no le muevan jala si esta lista, supongo que las calificaciones 
            var calificaciones = reposCalificacion.GetAll().Where(x => x.IdAlumnoNavigation.IdGrupo == idGrupo && (x.Calificacion1 < 6 || x.Calificacion1 < 60)).ToList();
            var alumnos = reposCalificacion.GetAll().Where(x => x.IdAlumnoNavigation.IdGrupo == idGrupo && (x.Calificacion1 < 6 || x.Calificacion1 < 60))
                .Select(x => x.IdAlumnoNavigation.Nombre).ToList();
            var asignaturas = reposCalificacion.GetAll().Where(x => x.IdAlumnoNavigation.IdGrupo == idGrupo && (x.Calificacion1 < 6 || x.Calificacion1 < 60))
                .Select(x => x.IdAsignaturaNavigation.Nombre).ToList();
            for (int i = 0; i < calificaciones.Count; i++)
            {
                alumnosreporbados.Add(new ReporteDTO()
                {
                    Nombre = alumnos.ToList()[i],
                    Asignatura = asignaturas.ToList()[i],
                    Calificacion = calificaciones.ToList()[i]
                });
            }
            return Ok(alumnosreporbados);
        }
        //podemos manejar dtos, creo que seria lo mejor pero ira, sin manos
        [HttpPost("/AgegarCalificacion")]
        public IActionResult AgegarCalificacion(CalificacionDTO calif)
        {
            if (calif == null)
                return BadRequest("Esto no deberia salir de hecho, probablemente error de no se, envio desde la app, checar que el objeto enviado sea 'CalificacionDTO'");
            calif.Calificacion.IdPeriodo = 2;
            reposCalificacion.Insert(calif.Calificacion);
            var aja = docenterepo.GetAll().Where(x => x.IdAlumno == calif.Calificacion.IdAlumno).FirstOrDefault();
            if (aja == null)
                docenterepo.Insert(new DocenteAlumno()
                {
                    IdDocente = calif.Calificacion.IdDocente,
                    IdAlumno = calif.Calificacion.IdAlumno,
                    IdPeriodo = calif.Calificacion.IdPeriodo,
                    ComentarioDocente = calif.Comentario
                });
            if (calif.Comentario != null && aja != null)
            {
                aja.ComentarioDocente = calif.Comentario;
                docenterepo.Update(aja);
            }
            return Ok();
        }
        //Asi sin autorizacion pal alumno que se meta de jaker se ponga 10
        [HttpPut("/EditarCalificacion")]
        public IActionResult EditarCalificacion(CalificacionDTO calif)
        {
            //Tenemos de 2 hacemos las validaciones aqui o en la app, lo mejor seria en las 2 pero bueno eso seria avaricia y eso es un pecado
            if (calif == null)
                return BadRequest();
            var a = reposCalificacion.GetById(calif.Calificacion.Id);
            if (a == null)
                return NotFound();
            else
            {
                a.Calificacion1 = calif.Calificacion.Calificacion1;
                a.Unidad = calif.Calificacion.Unidad;
                a.IdDocente = calif.Calificacion.IdDocente;
                a.IdPeriodo = calif.Calificacion.IdPeriodo;
                a.IdAlumno = calif.Calificacion.IdAlumno;
            }
            reposCalificacion.Update(a);
            var aja = docenterepo.GetAll().Where(x => x.IdAlumno == calif.Calificacion.IdAlumno).FirstOrDefault();
            if (calif.Comentario != null && aja != null)
            {
                aja.ComentarioDocente = calif.Comentario;
                docenterepo.Update(aja);
            }
            return Ok();
        }
    }
}
