using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using System.Data.Objects.SqlClient;
using DoAn.Models.Authentication;
using System.IO;

namespace DoAn.Controllers
{
    public class ProductsController : Controller
    {
        
        GiayEntities db = new GiayEntities();
        // GET: /Products/
        [Authentication]

        public ActionResult Index(string search = "", int page = 1)
        {
            //if (maloai != 0)
            //{
            //    List<Product> product = db.Products
            //    .Where(p => SqlFunctions.PatIndex("%" + search + "%", p.ProductName) > 0)
            //    .Where(p => p.BrandID == maloai)
            //    .ToList();
            //    int SoTrangHienThi = 6;
            //    int SoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(product.Count) / Convert.ToDouble(SoTrangHienThi)));
            //    int BoQua = (page - 1) * SoTrangHienThi;
            //    ViewBag.Page = page;
            //    ViewBag.SoTrang = SoTrang;

            //    product = product.Skip(BoQua).Take(SoTrangHienThi).ToList();

            //    return View(product);
            //}
            //else
            //{
                List<Product> product = db.Products
                    .Where(p => SqlFunctions.PatIndex("%" + search + "%", p.ProductName) > 0)
                    .ToList();

                int SoTrangHienThi = 6;
                int SoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(product.Count) / Convert.ToDouble(SoTrangHienThi)));
                int BoQua = (page - 1) * SoTrangHienThi;
                ViewBag.Page = page;
                ViewBag.SoTrang = SoTrang;

                product = product.Skip(BoQua).Take(SoTrangHienThi).ToList();

                return View(product);
            //}

        }

        public ActionResult XemChiTiet(int msp)
        {
            Product sp = db.Products.Single(s => s.ProductID == msp);
            if (msp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }


        public ActionResult Create()
        {
            List<Brand> brand = db.Brands.ToList();
            ViewBag.Brand = brand;
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            try
            {
                if (p.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(p.ImageUpload.FileName);
                    string extension = Path.GetExtension(p.ImageUpload.FileName);
                    fileName = fileName + extension;
                    p.ProductImage = fileName;
                    p.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Contents/images/"), fileName));
                }
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            //db.Products.Add(p);
            //db.SaveChanges();
            //return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            List<Brand> brand = db.Brands.ToList();
            ViewBag.Brand = brand;
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.ProductImage = fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Contents/images/"), fileName));
                }
                db.Entry(pro).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            //    Product product = db.Products.Where(p => p.ProductID == pro.ProductID).FirstOrDefault();
            //    product.ProductName = pro.ProductName;
            //    product.ProductImage = pro.ProductImage;
            //    product.Description = pro.Description;
            //    product.BrandID = pro.BrandID;
            //    product.Price = pro.Price;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
        }
        public ActionResult delete(int id)
        {
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult delete(int id, Product pro)
        {
            pro = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SPTheoLoai(int maloai)
        {
            var sp = db.Products.Where(s => s.BrandID == maloai).ToList();
            int sl = db.Products.Count(s => s.BrandID == maloai);
            if (sl == 0)
            {
                ViewBag.TB = "Không có sản phẩm nào thuộc loại này!";
            }
            else
            {
                ViewBag.TB = null;
            }
            return View(sp);
        }
    }
}
