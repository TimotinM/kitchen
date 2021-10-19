using Kitchen.Enums;
using Newtonsoft.Json;

namespace Kitchen.Models
{
     public class CookingDetails
     {
          public readonly int FoodId;
          public readonly int CookId;
          public CookingStatusEnum Status { get; set; }

          public CookingDetails(int foodId, int cookId)
          {
               FoodId = foodId;
               CookId = cookId;
          }
     }
}
