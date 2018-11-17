﻿namespace ADondeIr.Frontend.Controllers
{
    using System.Web.Mvc;
    using BusinessLogic;
    using Common.Extensions;
    using Common.Model;
    using Common.Session;
    using Model;

    [Autenticado(IsAdmin = true)]
    public class UsuarioController : BaseController
    {
        private readonly UsuarioBl _bl = new UsuarioBl();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listado()
        {
            return Json(new { data = _bl.GetAll() }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PartialMantenimiento(int id = 0)
        {
            Usuario model = null;
            if (id != 0) model = _bl.Get(id);
            return PartialView("_Mantenimiento", model ?? new Usuario());
        }
        [HttpPost]
        public JsonResult Mantenimiento(Usuario model)
        {
            Result result;
            if (ModelState.IsValid)
            {
                if (model.pkUsuario != 0)
                {
                    model.fkUsuarioEdita = GetUser<Usuario>().pkUsuario;
                }
                else
                {
                    model.fkUsuarioCrea = GetUser<Usuario>().pkUsuario;
                }
                result = _bl.Save(model);

            }
            else
            {
                result = new Result { Errors = ModelState.AllErrors() };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            return Json(_bl.Delete(id, GetUser<Usuario>().pkUsuario), JsonRequestBehavior.AllowGet);
        }
    }
}