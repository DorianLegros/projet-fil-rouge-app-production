using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetFilBleu_AppProduction.Classes
{
    class ArticleRecipeLayerElement
    {
        public int IdFirstComponent { get; set; }
        public int QuantityFirstComponent { get; set; }
        public int IdSecondComponent { get; set; }
        public int QuantitySecondComponent { get; set; }
        public int IdOperation { get; set; }
        public bool Done { get; set; } = false;
    }
}
