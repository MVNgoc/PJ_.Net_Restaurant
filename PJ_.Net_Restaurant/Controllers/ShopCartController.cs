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
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Session["cart"] = cart;
            }
            return View();
        }

        public ActionResult AddToCart(int productID)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Item item = new Item();
                item.food = db.Foods.Find(productID);
                item.quantity = 1;
                item.price = 0;
                item.currentDateTime = DateTime.Now;
                cart.Add(item);
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = IsInCart(productID);
                if (index != -1)
                {
                    cart[index].quantity++;
                }
                else
                {
                    cart.Add(new Item() { food = db.Foods.Find(productID), quantity = 1, price = 0, currentDateTime = DateTime.Now });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public int IsInCart(int productID)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].food.id == productID)
                {
                    return i;
                }
            }

            return -1;
        }

        public ActionResult removeFromCart(int productID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = IsInCart(productID);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}