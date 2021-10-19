using Kitchen.Models;
using Kitchen.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Controllers
{
     public class OrderController : Controller
     {
          public IActionResult Index()
          {
               return View();
          }

          [HttpPost("order")]
          public IActionResult Order([FromBody] Distribution order)
          {
              if(order != null)
               {
                    OrderList.Instance.Orders.Enqueue(order);
                    Console.WriteLine($"Kitchen recived the order with id-{order.order_id}!");
                    return Ok();
               }
               return BadRequest();
          }
     }
}
