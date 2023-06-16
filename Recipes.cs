using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public class Recipes : IEnumerable<Recipe> {
    public List<Recipe> RecipesList { get; set; } = new List<Recipe>();

    public Recipes() {
      RecipesList = ReadRecipes();
    }

    public List<Recipe> ReadRecipes() {
      List<Recipe> RecipesList = new List<Recipe>();

      StreamReader SR = new StreamReader(@"recipes.txt");

      string RecipeString;
      for (int RecipeNumber = 0; (RecipeString = SR.ReadLine()) != null; ++RecipeNumber) {
        Recipe RecipeSingle = new Recipe();

        RecipeSingle.Name = RecipeString.Split('~')[0].Replace("[", "").Replace("]", "");

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

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    public IEnumerator<Recipe> GetEnumerator() {
      for (int RecipeCurrent = 0; RecipeCurrent < RecipesList.Count; ++RecipeCurrent) {
        yield return RecipesList[RecipeCurrent];
      }
    }

    public override string ToString() {
      string RecipesStr = string.Empty;

      foreach (var Recipe in RecipesList) {
        RecipesStr += Recipe.ToString() + '\n';
      }

      return RecipesStr;
    }

    public Recipes Search(string WordSearch) {
      List<Recipe> RecipesForSearch = new List<Recipe>();

      foreach (var Recipe in RecipesList) {  
        if (Recipe.Name.Contains(WordSearch)) {
          RecipesForSearch.Add(Recipe);
        }
      }

      if (RecipesForSearch.Count == 0) {
        return null;
      }

      return new Recipes { RecipesList = RecipesForSearch };
    }
  }
}
