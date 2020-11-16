using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopPage
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        public int ID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
    }
}