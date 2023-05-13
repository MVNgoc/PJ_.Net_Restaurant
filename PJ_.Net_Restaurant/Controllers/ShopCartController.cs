using PJ_.Net_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Controllers
{
    public class ShopCartController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();
        // GET: ShopCart
        public ActionResult Index()
        {
            if(Session["Cart"] == null)
            {
                return RedirectToAction("Index");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult AddToCart(int productID)
        {
            var pro = db.Foods.SingleOrDefault(s => s.id == productID);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (Session["cart"] == null || cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;

        }

        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["Id_product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)
            {
                _t_item = cart.total_quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }

    }
}