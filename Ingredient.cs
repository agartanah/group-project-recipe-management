using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public class Ingredient {
    public string Name { get; set; }
    public string Weight { get; set; }

    public Ingredient() { }
    
    public Ingredient(string name, string weight) {
      Name = name;
      Weight = weight;
    }

    public override string ToString() {
      return Name + " " + Weight;
    }
  }
}
