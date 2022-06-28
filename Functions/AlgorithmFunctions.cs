using ProjetFilBleu_AppProduction.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetFilBleu_AppProduction.Functions
{
    static class AlgorithmFunctions
    {
        public static Dictionary<int, List<ArticleRecipeLayerElement>> AddLayerRecipes(ArticleProductionTreeElement articleProductionTreeElement, Dictionary<int, List<ArticleRecipeLayerElement>> recipeLayersDic, int layer = 1)
        {
            try
            {
                if (articleProductionTreeElement.Recipe == null)
                    return recipeLayersDic;

                if (!recipeLayersDic.ContainsKey(layer))
                    recipeLayersDic.Add(layer, new List<ArticleRecipeLayerElement>());

                ArticleRecipeLayerElement layerEl = new ArticleRecipeLayerElement { IdArticle = articleProductionTreeElement.Id, Quantity = articleProductionTreeElement.Quantity, Code = articleProductionTreeElement.Code, CodeFirstComponent = articleProductionTreeElement.Recipe.FirstComponent.Code, QuantityFirstComponent = articleProductionTreeElement.Recipe.FirstComponent.Quantity, IdOperation = (int)articleProductionTreeElement.Recipe.OperationId };

                if (articleProductionTreeElement.Recipe.SecondComponent != null)
                {
                    layerEl.CodeSecondComponent = articleProductionTreeElement.Recipe.SecondComponent.Code;
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

        public static List<string> LaunchProduction(Dictionary<int, List<ArticleRecipeLayerElement>> recipeLayersDic)
        {
            List<string> logs = new List<string>();
            try
            {
                foreach (var layer in recipeLayersDic.Reverse())
                {
                    List<ArticleRecipeLayerElement> layerRecipes = layer.Value;
                    foreach (var recipe in layerRecipes)
                    {
                        if (recipe.Done)
                            continue;

                        List<List<ArticleRecipeLayerElement>> duplicatedRecipesLayerLists = recipeLayersDic.Where(rld => rld.Value.Where(r => r.IdArticle == recipe.IdArticle && !r.Done).Any()).Select(rld => rld.Value).ToList();
                        List<ArticleRecipeLayerElement> duplicatedRecipes = new List<ArticleRecipeLayerElement>();
                        foreach (var duplicatedRecipeLayerList in duplicatedRecipesLayerLists)
                            duplicatedRecipes.AddRange(duplicatedRecipeLayerList.Where(drecipe => drecipe.IdArticle == recipe.IdArticle));

                        logs.Add("Lancement de la production de " + duplicatedRecipes.Sum(dr => dr.Quantity) + " articles code " + duplicatedRecipes.First().Code + "...");
                        logs.Add("Utilisation de " + duplicatedRecipes.Sum(dr => dr.QuantityFirstComponent) + " articles code " + duplicatedRecipes.First().CodeFirstComponent);
                        if (duplicatedRecipes.First().CodeFirstComponent != null)
                            logs.Add("Utilisation de " + duplicatedRecipes.Sum(dr => dr.QuantitySecondComponent) + " articles code " + duplicatedRecipes.First().CodeSecondComponent);
                        // appel à l'API de Jad pour définir productionResult (route production POST)
                        bool productionResult = true;
                        if (!productionResult)
                        {
                            logs.Add("Une erreur est survenue lors de la production des articles. Arrêt de la procédure.");
                            return logs;
                        }
                            
                        logs.Add("Production des articles effectuée avec succès.");
                        foreach (var producedRecipe in duplicatedRecipes)
                            producedRecipe.Done = true;
                    }
                }

                return logs;
            }
            catch (Exception e)
            {
                logs.Add("Une erreur fatale est survenue lors de la production. Arrêt de la procédure.");
                return logs;
            }
        }
    }
}
