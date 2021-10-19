using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kitchen.Models
{
     public class Distribution
     {
          public int order_id { get; set; }
          public int table_id { get; set; }
          public int waiter_id { get; set; }
          public int[] items { get; set; }
          public int priority { get; set; }
          public float max_wait { get; set; }
          public Int32 pick_up_time { get; set; }
          public Int32 cooking_time { get; set; }

          public List<CookingDetails> cooking_details = new List<CookingDetails>();

     }
}
