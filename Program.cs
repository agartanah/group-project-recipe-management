using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  internal class Program {
    static void Main(string[] args) {
      //string RecipesString = File.ReadAllText(@"recipes.txt");

      //string[] RecipesArr = RecipesString.Split('&');

      //foreach (string Recipe in RecipesArr) {
      //  Console.Write(Recipe);
      //}

      //Console.ReadKey();

      //FileInfo file = new FileInfo(@"recipes.txt");

      //Console.WriteLine(file.Name);

      //Recipes RecipesObj = new Recipes();

      //Console.WriteLine(RecipesObj);

      //Recipes ResipesSearch = RecipesObj.Search("Салат");

      //Console.WriteLine(ResipesSearch);

      Controller.ReadRecipes();

      Console.WriteLine(Controller.RecipesObj);

      string Name = "Суши", Description = "Кладём рыбу в рис. Готово.";
      List<string> Ingredients = new List<string> {
        "Рыба 1кг",
        "Рис 1кг"
      };

      Controller.CreateRecipe(Name, Ingredients, Description);

      Console.WriteLine(Controller.RecipesObj);

      Console.ReadKey();
    }
  }
}
