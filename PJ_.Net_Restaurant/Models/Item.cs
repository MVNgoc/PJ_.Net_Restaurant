using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PJ_.Net_Restaurant.Models
{
    public class Item
    {
        public Food food { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }
        public DateTime currentDateTime { get; set; }
    }
}