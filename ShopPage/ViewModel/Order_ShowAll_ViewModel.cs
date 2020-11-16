using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopPage.ViewModel
{
    public class Order_ShowAll_ViewModel
    {

        public int ID { get; set; }

        public string UserName { get; set; }



        public int OrderNumber { get; set; }


        public double SalesTax { get; set; }
        public double Freight { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Fulfill { get; set; }
        public bool Paid { get; set; }


    }
}