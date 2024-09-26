using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.Models.Authentication;

namespace DoAn.Controllers
{
    public class BrandsController : Controller
    {
        //
        // GET: /Brands/
        
        GiayEntities db = new GiayEntities();
        [Authentication]
        // GET: /Products/

        public ActionResult Index()
        {
            List<Brand> brand = db.Brands.ToList();
            return View(brand);
        }

        public ActionResult Create()
        {
            List<Brand> brand = db.Brands.ToList();
            return View(brand);
        }

        [HttpPost]
        public ActionResult Create(Brand bra)
        {
            try
            {
                db.Brands.Add(bra);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception er)
            {
                return Json(new { result = false, error = er.Message });
            }

        }
        //public ActionResult Create(Brand p)
        //{
        //    db.Brands.Add(p);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        public ActionResult Edit(int id)
        {
            Brand bra = db.Brands.Where(p => p.BrandID == id).FirstOrDefault();
            return View(bra);
        }

        [HttpPost]
        public ActionResult Edit(Brand b)
        {
            Brand bra = db.Brands.Where(p => p.BrandID == b.BrandID).FirstOrDefault();
            bra.BrandName = b.BrandName;
            db.SaveChanges();
            return RedirectToAction("Index", "Brands");
        }

        public ActionResult delete(int id)
        {
            Brand bra = db.Brands.Where(p => p.BrandID == id).FirstOrDefault();
            db.Brands.Remove(bra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
