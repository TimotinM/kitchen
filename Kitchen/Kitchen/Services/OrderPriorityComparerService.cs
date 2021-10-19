using System.Collections.Generic;

namespace Kitchen.Models
{
     class OrderPriorityComparerService : IComparer<Distribution>
     {
          public int Compare(Distribution x, Distribution y)
          {
               if (x.priority * x.pick_up_time > y.priority * y.pick_up_time) return 1;
               if (x.priority * x.pick_up_time < y.priority * y.pick_up_time) return -1;
               return 0;
          }
     }
}