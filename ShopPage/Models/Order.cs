using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopPage
{
    [Table("Order")]
    public class Order
    {
        public int ID { get; set; }

        //[ForeignKey("Customer")]
        //public int CustomerID { get; set; }
        //public Customer Customer { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }


        public int OrderNumber { get; set; }

        //[ForeignKey("Payment")]
        //public int PaymentID { get; set; }
        //public Payment Payment { get; set; }

        //public DateTime ShipDate { get; set; }

        //public double SalesTax { get; set; } = 14;

        public double SalesTax { get; set; }
        public double Freight { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Fulfill { get; set; }
        public bool Paid { get; set; }

        //public DateTime PaymentDate { get; set; }


        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}