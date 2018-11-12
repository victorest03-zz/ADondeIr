﻿namespace ADondeIr.Frontend.Controllers
{
    using BusinessLogic;
    using Common.Session;
    using System.Web.Mvc;
    using Model;

    public class LoginController : BaseController
    {
        private readonly UsuarioBl _bl = new UsuarioBl();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = _bl.ValidarUser(model.cEmail, model.cPassword);
                if (user != null)
                {
                    SessionHelper.AddUserToSession(user);
                    if (model.isRememberMe)
                        SessionHelper.AddCookieToClave(user.pkUsuario.ToString());
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("ErrorSesion", "El usuario o la contraseña ingresados no son correctos");
            }

            return View(model);
        }

        [Autenticado]
        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSessionAndCookie();
            return RedirectToAction("Index");
        }
    }
}