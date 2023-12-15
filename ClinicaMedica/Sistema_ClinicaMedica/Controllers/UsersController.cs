using Sistema_ClinicaMedica.Models;
using Sistema_ClinicaMedica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ClinicaMedica.Controllers
{
    public class UsersController : Controller
    {
        ProjectContext ProjectContext;
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: LoginController/Create
        public ActionResult Login()
        {
            HttpContext.Response.Cookies.Delete("UserId");
            ViewBag.flatLogin = true;
            return View();
        }


        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users usuario)
        {
            try
            {
                ProjectContext = new ProjectContext(usuario.user, usuario.pass);
                Operation operacion = ProjectContext.validaConexion();
                ViewBag.flatLogin = operacion.esValida;
                ViewBag.Error = operacion.Mensaje;

                if (ViewBag.flatLogin)
                {

                    HttpContext.Response.Cookies.Append("UserId", usuario.user);
                    //return Redirect("/Sistema_ClinicaMedica"); // antes de publicar
                    return Redirect("/" + ProjectContext.getSitio()); // pruebas en el proyecto
                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
