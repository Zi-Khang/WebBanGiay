using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class APIController : ApiController
    {
        GiayEntities db = new GiayEntities();
        [HttpGet]
        public IEnumerable<itemSP> GetAll()
        {
            var item = (from p in db.Products
                        select new itemSP
                        {
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            ProductImage = p.ProductImage,
                            Description = p.Description,
                            Price = p.Price,
                            BrandID = p.BrandID
                        }).ToList();
            return item;
        }

        [HttpGet]
        public IEnumerable<itemSP> GetSPByBraID(int BraID)
        {
            var item = (from p in db.Products
                        where p.BrandID == BraID
                        select new itemSP
                        {
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            ProductImage = p.ProductImage,
                            Description = p.Description,
                            Price = p.Price,
                            BrandID = p.BrandID
                        }).ToList();
            return item;
        }
        //public List<Product> danhSachSinhVien()
        //{
        //    return db.Products.ToList();
        //}
        //[HttpGet]
        //public Product timSP(int maSP)
        //{
        //    return db.Products.SingleOrDefault(sp => sp.ProductID == maSP);
        //}
        //[HttpPost]
        //public bool ThemSP([FromBody] Product p)
        //{
        //    try
        //    {
        //        db.Products.InsertOnSubmit(p);
        //        db.SubmitChanges();
        //        return true;
        //    }
        //}
    }
}
