using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public class RecipeCreateCommand : ICommand {
    Recipes recipes;

    public RecipeCreateCommand(Recipes RecipesObj) { 
      recipes = RecipesObj;
    }

    public bool Execute(Recipe RecipeObj) {
      return recipes.CreateRecipe(RecipeObj);
    }

    public bool Undo(Recipe RecipeObj) {
      return recipes.DeleteRecipe(RecipeObj);
    }
  }

  public interface ICommand {
    bool Execute(Recipe RecipeObj);
    bool Undo(Recipe RecipeObj);
  }
}
