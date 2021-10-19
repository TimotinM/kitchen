using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Models
{
     public class Order
     {
          public int order_id { get; set; }
          public int table_id { get; set; }
          public int waiter_id { get; set; }
          public int[] items { get; set; }
          public int priority { get; set; }
          public float max_wait { get; set; }
          public Int32 pick_up_time { get; set;}
     }
}
     