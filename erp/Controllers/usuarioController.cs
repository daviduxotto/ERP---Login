using erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace erp.Controllers
{
    public class usuarioController : Controller
    {
        // GET: usuario
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<ActionResult> verificarUsuario(usuario parusuario)
        {
            salida varSalida = new salida();
            varSalida = parusuario.loginUsuario(parusuario);
            if (varSalida.retorno >= 1)
                Session["token"] = varSalida.token;
            return Json(varSalida, JsonRequestBehavior.AllowGet);
        }
    }
}