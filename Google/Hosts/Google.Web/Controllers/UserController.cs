using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Google.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Add()
        {
            string userId = ECommon.Utilities.ObjectId.GenerateNewStringId();
            return Content("1");
        }
        public ActionResult Del()
        {
            return Content("1");
        }
        public ActionResult Upd()
        {
            return Content("1");
        }
        public ActionResult Get()
        {
            return Content("1");
        }
    }
}