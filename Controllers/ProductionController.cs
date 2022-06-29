using AlgorithmAppProduction.Classes;
using AlgorithmAppProduction.Functions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgorithmAppProduction.Controllers
{
    [ApiController]
    [Route("production")]
    public class ProductionController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductionTreeElement productionTree) {
            try
            {
                //ProductionTreeElement productionTree = JsonConvert.DeserializeObject<ProductionTreeElement>(productionTreeJson);

                Dictionary<int, List<ArticleRecipeLayerElement>> productionLayersDic = new Dictionary<int, List<ArticleRecipeLayerElement>>();
                productionLayersDic = AlgorithmFunctions.AddLayerRecipes(productionTree, productionLayersDic);
                if (productionLayersDic == null)
                    return new BadRequestObjectResult("Une erreur s'est produite lors de l'initialisation du parcours de production.");

                bool productionResult = await AlgorithmFunctions.LaunchProduction(productionLayersDic);
                if (!productionResult)
                    return new BadRequestObjectResult("Une erreur s'est produite lors de la production.");

                return new OkResult();
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return new OkResult();
        }
    }
}
