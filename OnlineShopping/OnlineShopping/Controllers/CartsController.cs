using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class CartsController : ApiController
    {
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();

        #region Cart
        public IHttpActionResult AddToCart(Cart cart)
        {
            Cart objcl = new Cart();
            objcl.ProductID = cart.ProductID;
            objcl.TotalPrice = cart.TotalPrice;
            objcl.UserID = cart.UserID;
            db.Carts.Add(objcl);
            db.SaveChanges();
            return Ok("Success");
        }
        #endregion


    }
}