using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project_recipe_management {
  public interface IObserver {
    string Update(string RecipeName);
  }

  public class ObserveRecipeCreate : IObserver {
    public string Update(string RecipeName) {
      return $"Рецепт под названием \"{RecipeName}\" был создан!";
    }
  }

  public class ObserveRecipeDelete : IObserver {
    public string Update(string RecipeName) {
      return $"Рецепт под названием \"{RecipeName}\" был удалён!";
    }
  }
}
