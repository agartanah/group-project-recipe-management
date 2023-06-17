﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  interface IRecipes {
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    string Notify(string ProductName);
  }

  public class Recipes : IEnumerable<Recipe>, IRecipes {
    public List<Recipe> RecipesList { get; set; } = new List<Recipe>();
    private IObserver observer;

    public Recipes() {
      RecipesList = ReadRecipes();
    }

    public List<Recipe> ReadRecipes() {
      List<Recipe> RecipesList = new List<Recipe>();

      string[] RecipesString = File.ReadAllText(@"recipes.txt").Split('&');

      for (int RecipeNumber = 0; RecipeNumber < RecipesString.Length - 1; ++RecipeNumber) {
        Recipe RecipeSingle = new Recipe();
        string RecipeString = RecipesString[RecipeNumber];

        RecipeSingle.Name = RecipeString.Split('~')[0].Replace("[", "").Replace("]", "").Replace("\n", "");

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

    public void Attach(IObserver Observer) {
      observer = Observer;
    }

    public void Detach(IObserver Observer) {
      observer = Observer;
    }

    public string Notify(string RecipeName) {
      return observer.Update(RecipeName);
    }

    public override string ToString() {
      string RecipesStr = string.Empty;

      foreach (var Recipe in RecipesList) {
        RecipesStr += Recipe.ToString() + '\n';
      }

      return RecipesStr;
    }

    public bool CreateRecipe(Recipe RecipeObj) {
      RecipesList.Add(RecipeObj);
      SaveRecipes();

      return true;
    }

    private bool SaveRecipes() {
      string RecipesSave = string.Empty;

      foreach (var Recipe in this) {
        RecipesSave += Recipe.ToString() + "\n";
      }

      File.WriteAllText(@"recipes.txt", RecipesSave);

      return true;
    }

    public bool DeleteRecipe(Recipe RecipeObj) {
      if (RecipesList.Remove(RecipeObj)) {
        SaveRecipes();

        return true;
      }
      
      return false;
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
