using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopPage.ViewModel
{


    public class ItemQuentety
    {
        public Item item;
        public int count = 0;

        public double total
        {
            get
            {
                return item.Price * count;
            }
        }
    }
}