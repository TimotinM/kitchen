using Kitchen.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Services
{
     public class OrderServices
     {
          public string GetOrder(Distribution order)
          {
               if(order != null)
               {
                    return "ok";
               }
               return "bad";
          }
     }
}
