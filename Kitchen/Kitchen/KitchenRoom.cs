using Kitchen.Enums;
using Kitchen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace Kitchen
{
     class KitchenRoom
     {
          private List<Cook> _cooks;
          private static List<CookingAparatus> _cookingAparatus;
          private List<MenuItem> _menuItems = Menu.Instance.MenuItems;
          private static Mutex _mut = new();

          public KitchenRoom(List<Cook> cooks, List<CookingAparatus> cookingAparatus)
          {
               _cooks = cooks;
               _cookingAparatus = cookingAparatus;
               Thread t = new(new ThreadStart(() => Start()));
               t.Start();
          }

          private void Start()
          {
               while (true)
               {
                    foreach (var order in OrderList.Instance.Orders.ToList())
                    {
                         if (order == null) continue;
                         if (order.cooking_details.Count == order.items.Length)
                         {
                              var isOrderReady = order.cooking_details.ToList().All(c => c.Status == CookingStatusEnum.Ready);
                              if (isOrderReady)
                              {
                                   order.cooking_time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - order.pick_up_time;
                                   Console.WriteLine($"Order {order.order_id} is ready in {order.cooking_time}");
                                   using (var client = new HttpClient())
                                   {
                                        var postTask = client.PostAsJsonAsync<Distribution>("http://localhost:5001/distribution", order);
                                        postTask.Wait();

                                        var result = postTask.Result;
                                   }
                                   OrderList.Instance.Orders.Remove(order);
                              }
                         }
                    }
               }
          }

          public static void PickUpOrderItem(Cook cook)
          {
               _mut.WaitOne();
               if (cook.ProficiencySemaphore.WaitOne(0))
               {
                    var order = OrderList.Instance.Orders.FirstOrDefault();
                    if (order == null)
                    {
                         cook.ProficiencySemaphore.Release(1);
                         _mut.ReleaseMutex();
                         return;
                    }
                    var availableCookingAparatus = _cookingAparatus.Where(a => a.State == CookingAparatusStateEnum.Free).Select(a => a.Name).ToList();
                    var unpreparedOrderIds = order.items.Except(order.cooking_details.Select(d => d.FoodId)).ToList();
                    var test = unpreparedOrderIds
                         .Select(itemId => Menu.Instance.MenuItems.Single(menuItem => menuItem.Id == itemId))
                         .Where(item => item.Complexity <= (int)cook.Rank && (item.CookingAparatus == null || availableCookingAparatus.Contains(item.CookingAparatus)))
                         .OrderBy(item => item.Complexity).ToList();

                    var menuOrder = test
                              .FirstOrDefault();
                    if (menuOrder == null)
                    {
                         cook.ProficiencySemaphore.Release(1);
                         _mut.ReleaseMutex();
                         return;
                    }

                    cook.CookItemHandler(order, menuOrder, menuOrder.CookingAparatus != null ? _cookingAparatus.Single(a => a.Name == menuOrder.CookingAparatus) : null);
               }
               _mut.ReleaseMutex();
          }
     }
}
