using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PJ_.Net_Restaurant.Models
{
    public class ProductModel
    {
        public List<FoodStyle> foodStyles { get; set; }
        public List<Food> foods { get; set; }
    }
}