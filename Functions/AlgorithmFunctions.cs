using ProjetFilBleu_AppProduction.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetFilBleu_AppProduction.Functions
{
    class AlgorithmFunctions
    {
        public Dictionary<int, List<ArticleRecipeLayerElement>> AddLayerRecipes(ArticleProductionTreeElement articleProductionTreeElement, Dictionary<int, List<ArticleRecipeLayerElement>> recipeLayersDic, int layer = 1)
        {
            try
            {
                if (articleProductionTreeElement.Recipe == null)
                    return recipeLayersDic;

                if (!recipeLayersDic.ContainsKey(layer))
                    recipeLayersDic.Add(layer, new List<ArticleRecipeLayerElement>());

                ArticleRecipeLayerElement layerEl = new ArticleRecipeLayerElement { IdFirstComponent = articleProductionTreeElement.Recipe.FirstComponent.Id, QuantityFirstComponent = articleProductionTreeElement.Recipe.FirstComponent.Quantity, IdOperation = (int)articleProductionTreeElement.Recipe.OperationId };

                if (articleProductionTreeElement.Recipe.SecondComponent != null)
                {
                    layerEl.IdSecondComponent = articleProductionTreeElement.Recipe.SecondComponent.Id;
                    layerEl.QuantitySecondComponent = articleProductionTreeElement.Recipe.SecondComponent.Quantity;
                }

                recipeLayersDic[layer].Add(layerEl);

                recipeLayersDic = AddLayerRecipes(articleProductionTreeElement.Recipe.FirstComponent, recipeLayersDic, layer + 1);
                if (articleProductionTreeElement.Recipe.SecondComponent != null)
                    recipeLayersDic = AddLayerRecipes(articleProductionTreeElement.Recipe.SecondComponent, recipeLayersDic, layer + 1);

                return recipeLayersDic;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
