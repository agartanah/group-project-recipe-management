using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public class Recipe : IEnumerable<Ingredient> {
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public string Description { get; set; }

    public Recipe() { }

    public Recipe(string recipeName, List<Ingredient> ingredients, string description) {
      Name = recipeName;
      Ingredients = ingredients;
      Description = description;
    }

    public IEnumerator<Ingredient> GetEnumerator() {
      for (int IngredientCurrent = 0; IngredientCurrent < Ingredients.Count; ++IngredientCurrent) {
        yield return Ingredients[IngredientCurrent];
      }
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    public override string ToString() {
      string RecipeStr = string.Empty;

      RecipeStr += $"{Name}: \n";

      foreach (var Ingredient in Ingredients) {
        RecipeStr += $" - {Ingredient}\n";
      }

      RecipeStr += Description;

      return RecipeStr;
    }
  }
}
