using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public class Recipe {
    public string RecipeName { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public string Description { get; set; }

    public Recipe() { }

    public Recipe(string recipeName, List<Ingredient> ingredients, string description) {
      RecipeName = recipeName;
      Ingredients = ingredients;
      Description = description;
    }

    public static List<Recipe> ReadRecipes() {
      List<Recipe> RecipesList = new List<Recipe>();

      StreamReader SR = new StreamReader(@"C:\Users\Егор\source\repos\group-project-recipe-management\recipe\recipes.txt");

      string RecipeString;
      for (int RecipeNumber = 0; (RecipeString = SR.ReadLine()) != null; ++RecipeNumber) {
        Recipe RecipeSingle = new Recipe();

        RecipeSingle.RecipeName = RecipeString.Split('~')[0].Replace("[", "").Replace("]", "");

        string[] RecipeIngredients = RecipeString
          .Split('~')[1]
          .Replace("[", "")
          .Replace("]", "")
          .Split(',');

        for (int IngredientNumber = 0; IngredientNumber < RecipeIngredients.Length; ++IngredientNumber) {
          Ingredient IngredientSingle = new Ingredient();

          IngredientSingle.Name = RecipeIngredients[IngredientNumber].Split('-')[0];
          IngredientSingle.Weight = RecipeIngredients[IngredientNumber].Split('-')[1];

          RecipeSingle.Ingredients.Add(IngredientSingle);
        }

        RecipeSingle.Description = RecipeString.Split('~')[2].Replace("[", "").Replace("]", "");

        RecipesList.Add(RecipeSingle);
      }

      SR.Close();

      return RecipesList;
    }
  }
}
