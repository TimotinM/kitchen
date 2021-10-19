using DotNetty.Common.Utilities;
using System;

namespace Kitchen.Models
{
     class OrderList
     {
          public PriorityQueue<Distribution> Orders = new(new OrderPriorityComparerService());
          private static readonly Lazy<OrderList> orderList = new(() => new OrderList());

          private OrderList()
          {
          }

          public static OrderList Instance
          {
               get
               {
                    return orderList.Value;
               }
          }
     }
}
