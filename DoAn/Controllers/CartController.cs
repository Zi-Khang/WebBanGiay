using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        GiayEntities db = new GiayEntities();

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<Cart>();
            }

            List<Cart> lst = Session["GioHang"] as List<Cart>;
            ViewBag.total = total();
            return View(lst);
        }

        public void Muasp(int id)
        {
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<Cart>();
            }
            List<Cart> lst = Session["GioHang"] as List<Cart>;
            bool flag = true;
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    if (item.masp == id)
                    {
                        item.sl++;
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                Cart cart = new Cart();
                Product sp = db.Products.FirstOrDefault(i => i.ProductID == id);
                cart.masp = sp.ProductID;
                cart.ten = sp.ProductName;
                cart.anh = sp.ProductImage;
                //cart.anh = sp.Anh;
                cart.sl = 1;
                cart.thanhtien = (sp.Price ?? 0) * cart.sl;
                cart.dongia = (sp.Price ?? 0);
                lst.Add(cart);
            }

            Session["GioHang"] = lst;

        }


        public ActionResult themVaoGioHang(int msp)
        {
            Muasp(msp);
            int masptemp = msp;
            return RedirectToAction("Index", "Products");
        }

        public ActionResult muaNgay(int msp)
        {
            Muasp(msp);

            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int msp)
        {
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<Cart>();
            }
            List<Cart> lst = Session["GioHang"] as List<Cart>;

            if (lst.Count > 0)
            {
                // Sử dụng phương thức RemoveAll để xóa tất cả các phần tử thỏa mãn điều kiện
                lst.RemoveAll(item => item.masp == msp);
            }

            Session["GioHang"] = lst;
            // Có thể thêm chuyển hướng hoặc cập nhật trang ở đây nếu cần
            return RedirectToAction("GioHang");
        }

        public Double total()
        {
            List<Cart> lst = Session["GioHang"] as List<Cart>;
            var total = (double)lst.Sum(s => s.dongia * s.sl);
            if (lst != null)
            {
                return total;
            }
            return 0; // Hoặc giá trị mặc định phù hợp
        }
        public int Amount()
        {
            List<Cart> lst = Session["GioHang"] as List<Cart>;
            var total = (int)lst.Sum(s => s.sl);
            if (lst != null)
            {
                return total;
            }
            return 0; // Hoặc giá trị mặc định phù hợp
        }
        public void clearCart()
        {
            List<Cart> lst = Session["GioHang"] as List<Cart>;
            lst.Clear();
        }
        public ActionResult Checkout()
        {
            try
            {
                List<Cart> lst = Session["GioHang"] as List<Cart>;
                Order order = new Order();
                int orderId;
                // Kiểm tra xem giá trị của session có khác null và có thể chuyển đổi thành int không
                if (HttpContext.Session["ID"] != null && int.TryParse(HttpContext.Session["ID"].ToString(), out orderId))
                {
                    order.UserID = orderId;
                    order.OrderDate = DateTime.Now;
                    order.TotalAmount = Amount();
                    order.OrderStatus = "Xử lý";
                    db.Orders.Add(order);
                    foreach (var item in lst)
                    {
                        OrderDetail orD = new OrderDetail();
                        orD.OrderID = order.OrderID;
                        orD.ProductID = item.masp;
                        orD.Quantity = item.sl;
                        orD.Price = item.dongia;
                        db.OrderDetails.Add(orD);
                    }
                    db.SaveChanges();
                    clearCart();
                    return RedirectToAction("GioHang", "Cart");
                }
                return RedirectToAction("GioHang", "Cart");

            }
            catch
            {
                return RedirectToAction("GioHang", "Cart");
            }
        }

    }
}
