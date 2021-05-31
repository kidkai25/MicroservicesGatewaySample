using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordermicroservice.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int UserID { get; set; }

        public double PriceAtPointInTime { get; set; }

        public DateTime OccuredAt { get; set; }

        public double Quantity { get; set; }

        public double Total { get; set; }
    }
}
