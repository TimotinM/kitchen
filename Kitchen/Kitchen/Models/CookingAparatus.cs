using Kitchen.Enums;

namespace Kitchen.Models
{
     class CookingAparatus
     {
          public CookingAparatusStateEnum State { get; set; } = CookingAparatusStateEnum.Free;
          public string Name { get; set; }

          public CookingAparatus(string name)
          {
               Name = name;
          }
     }
}
