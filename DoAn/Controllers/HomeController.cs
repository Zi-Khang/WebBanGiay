using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.Models.Authentication;
namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        
        GiayEntities db = new GiayEntities();
        [Authentication]
        //
        // GET: /Default1/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BrandPartial()
        {
            return View(db.Brands.ToList());
        }

    }
}
