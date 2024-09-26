using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class DonHangController : Controller
    {
        //
        // GET: /DonHang/
        GiayEntities db = new GiayEntities();
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
        
        public ActionResult comfirm(int id)
        {
            Order or = db.Orders.Where(p => p.OrderID == id).FirstOrDefault();
            or.OrderStatus = "Xác nhận";
            db.SaveChanges();
            return RedirectToAction("Index", "DonHang");
        }

        public ActionResult cancel(int id)
        {
            Order or = db.Orders.Where(p => p.OrderID == id).FirstOrDefault();
            return View(or);
        }
        [HttpPost]
        public ActionResult cancel(Order order)
        {
            Order or = db.Orders.Where(p => p.OrderID == order.OrderID).FirstOrDefault();
            or.OrderStatus = "Đã Hủy - " + order.OrderStatus;
            db.SaveChanges();
            return RedirectToAction("Index", "DonHang");
        }

        public ActionResult DHKH()
        {
            int orderId;
            
            if (HttpContext.Session["ID"] != null && int.TryParse(HttpContext.Session["ID"].ToString(), out orderId))
            {
                return View(db.Orders.Where(p => p.UserID == orderId).ToList());
            }
            return View();
        }

    }
}
