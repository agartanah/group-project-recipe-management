using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public static class Controller {
    static ICommand command;
    public static Recipes RecipesObj { get; set; }

    public static void ReadRecipes() {
      RecipesObj = new Recipes();
      SetCommand(new RecipeCreateCommand(RecipesObj));
    }

    /// <summary>
    ///  Устанавливает команду, которая будет выполняться, в данном классе это RecipeCreateCommand
    /// </summary>
    /// <param name="Command"></param>
    private static void SetCommand(ICommand Command) {
      command = Command;
    }

    /// <summary>
    /// Создание нового рецепта
    /// </summary>
    /// <param name="NameRecipe"></param>
    /// <param name="Ingredients"></param>
    /// <param name="DescriptionRecipe"></param>
    /// <returns></returns>
    public static string CreateRecipe(string NameRecipe, List<string> Ingredients, string DescriptionRecipe) {
      ExecuteCreate(NameRecipe, Ingredients, DescriptionRecipe);

      RecipesObj.Attach(new ObserveRecipeCreate());

      return RecipesObj.Notify(NameRecipe);
    }

    /// <summary>
    /// Создание рецепта с помощью паттерна Command
    /// </summary>
    /// <param name="NameRecipe"></param>
    /// <param name="Ingredients"></param>
    /// <param name="DescriptionRecipe"></param>
    /// <returns></returns>
    private static bool ExecuteCreate(string NameRecipe, List<string> Ingredients, string DescriptionRecipe) {
      Recipe RecipeObj = new Recipe {
        Name = NameRecipe,
        Description = DescriptionRecipe,
        Ingredients = new List<Ingredient>()
      };

      foreach (var Ingredient in Ingredients) {
        string NameIngredient = Ingredient.Split(' ')[0];
        string WeigthIngredient = Ingredient.Split(' ')[1];

        RecipeObj.CreateIngredient(NameIngredient, WeigthIngredient);
      }

      return command.Execute(RecipeObj);
    }

    /// <summary>
    /// Отменяет создание рецепта (то есть удаляет его)
    /// </summary>
    /// <param name="RecipeObj"></param>
    /// <returns></returns>
    public static bool CancelCreateRecipe(Recipe RecipeObj) {
      return UndoCreate(RecipeObj);
    }

    /// <summary>
    /// Отмена создания рецепта с помощью паттерна Command
    /// </summary>
    /// <param name="RecipeObj"></param>
    /// <returns></returns>
    private static bool UndoCreate(Recipe RecipeObj) {
      return command.Undo(RecipeObj);
    }

    /// <summary>
    /// Удаление рецепта
    /// </summary>
    /// <param name="RecipeObj"></param>
    /// <returns></returns>
    public static string DeleteRecipe(Recipe RecipeObj) {
      RecipesObj.DeleteRecipe(RecipeObj);

      RecipesObj.Attach(new ObserveRecipeDelete());

      return RecipesObj.Notify(RecipeObj.Name);
    }
  }
}
