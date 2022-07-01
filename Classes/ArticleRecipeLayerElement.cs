using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmAppProduction.Classes
{
    class ArticleRecipeLayerElement
    {
        public int IdArticle { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        //public int IdFirstComponent { get; set; }
        public int QuantityFirstComponent { get; set; }
        public string CodeFirstComponent { get; set; }
        //public int IdSecondComponent { get; set; }
        public int QuantitySecondComponent { get; set; }
        public string CodeSecondComponent { get; set; }
        public int IdOperation { get; set; }
        public bool Done { get; set; } = false;
    }
}
