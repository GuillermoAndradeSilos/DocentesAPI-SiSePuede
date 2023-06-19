using DocentesAPI_SiSePuede.Models;
using DocentesAPI_SiSePuede.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocentesAPI_SiSePuede.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// El controlador es eso, login, jaja salu2 a todos mis amigos
        /// de yutu
        /// PD. no le muevan a esto quien le mueva me lo funo.
        /// </summary>
        private readonly Sistem21PrimariaContext cx;
        private readonly Repository<Docente> docenterepos;
        private readonly Repository<Usuario> usuariorepos;

        public LoginController(Sistem21PrimariaContext cx)
        {
            this.cx = cx;
            docenterepos = new Repository<Docente>(this.cx);
            usuariorepos = new Repository<Usuario>(this.cx);
        }
        //El Usuario son las credenciales, quine lo diria tengo dislexia, no me salio
        [HttpPost("Login")]
        public IActionResult Login(Usuario login)
        {
            var usuario = usuariorepos.GetAll().Where(x => x.Usuario1 == login.Usuario1 && x.Contraseña == login.Contraseña).FirstOrDefault();
            if (usuario != null)
            {
                if (usuario.Rol != 2)
                    return Unauthorized("Tu no ere profe");
                else
                {
                    var docente = new Docente();
                    docente = docenterepos.GetAll().Where(x => x.IdUsuario == usuario.Id).FirstOrDefault();
                    if (docente != null)
                    {
                        docente.IdUsuarioNavigation = null;
                        if (docente.TipoDocente == 1)
                            return Ok(docente);
                        else
                            return BadRequest("No autorizo (insertar cara de enojao)");
                    }
                    else
                        return BadRequest("De hecho esto no deberia de salir, si sale algo anda mal, checar login, 3er if");
                }
            }
            else
                return BadRequest("Nombre de usuario o contraseña incorrecto");
        }
    }
}
