using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ProjetFilBleu_AppProduction
{
    class Program
    {
        // COTÉ ALGO
        // 1 var "couche" qui est à 1
        // on enregistre dans le dictionnaire le premier recipe 
        // -> [ valeur de couche : [ { "IdComposant1": 3, "IdComposant2": 4, "QuantiteComposant1": 3, "QuantiteComposant2": , "IdOperation": 2  } ]]
        // puis on rappelle méthode sur composant1 avec un "couche" + 1
        // et ainsi de suite
        // puis la même chose avec composant2 vec un "couche" + 1
        // et ainsi de suite
        //
        //
        // à la fin on se retrouve avec :
        //[ 1 : [ { "IdComposant1": 3, "IdComposant2": 4, "QuantiteComposant1": 3, "QuantiteComposant2": , "IdOperation": 2, "Fait": false  } ] ]
        // [ 2 : [ { "IdComposant1": 3, "IdComposant2": 4, "QuantiteComposant1": 3, "QuantiteComposant2": , "IdOperation": 2  }, { "IdComposant1": 3, "IdComposant2": 4, "QuantiteComposant1": 3, "QuantiteComposant2": , "IdOperation": 2  } ] ]
        // [ 3 : ... ]
        // on commence à 4 (dernier en gros)
        // on fait toutes les recettes de 4, mais à chaque recette on check si il n'y a pas de doublons sur d'autres couches ou la même couche pour les executer dans la foulée
        // on marque comme effectuée toute recette sur laquelle on passe y compris les doublons pour ne pas repasser dessus inutilement
        // une fois sur la couche 1, c'est la fin on se fait plaisir on se pipe de la plus belle des manières

        static void Main(string[] args)
        {
            var layers = 0;

            var jsonString = @"{
            ""Id"": 1,
            ""Quantite"": 7,
            ""Code"": ""P200"",
            ""Recipe"": {
                ""Operation"": 1,
                ""FirstComponent"": {
                    ""Id"": 2,
                    ""Code"": ""P3300"",
                    ""Quantity"": 7,
                    ""Recipe"": null
                },
                ""SecondComponent"": {
                    ""Id"": 3,
                    ""Code"": ""P998800"",
                    ""Quantity"": 6,
                    ""Recipe"": {
                        ""FirstComponent"": {
                        },
                        ""SecondComponent"": null
                    }
                }
            }
        }";

        var recipeObject = JsonSerializer.Deserialize<ArticleProductionTreeElement>(jsonString);

        // On crée un dictionnaire qui va contenir toutes les opérations
        Dictionary<int, List<ArticleProductionTreeElement>> recipe =
                new Dictionary<int, List<ArticleProductionTreeElement>>();

            // Boucle sur recipeObject pour enregistrer les opérations dans le dictionnaire
            foreach (var item in recipeObject.Recipe.Composant1.Recipe.Composant1)
            {
                if (recipe.ContainsKey(item.IdOperation))
                {
                    recipe[item.IdOperation].Add(item);
                }
                else
                {
                    recipe.Add(item.IdOperation, new List<ArticleProductionTreeElement> { item });
                }
            }

        // afficher les recettes du dictionnaire
        foreach (var item in recipe)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
    }
        
        // récusivité sur recipeObject pour enregistrer les opérations dans le dictionnaire
        
        private void SetArticleRecipe(ArticleProductionTreeElement recipeObject, Dictionary<int, List<ArticleProductionTreeElement>> recipe)
        {
            ArticleProductionTreeElementRecipe articleProductionTreeElementRecipe = new ArticleProductionTreeElementRecipe();
            ArticleProductionTreeElement firstComponent = new ArticleProductionTreeElement();
            ArticleProductionTreeElement secondComponent = new ArticleProductionTreeElement();
        }

    }

}