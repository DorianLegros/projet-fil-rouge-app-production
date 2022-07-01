using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmAppProduction.Classes
{
    public class ProductionOrdersToSend
    {
        public string codeWorkUnit { get; set; }
        public List<ProductionOrdersToSendOperation> productOperations { get; set; }
    }

    public class ProductionOrdersToSendOperation
    {
        public string codeArticle { get; set; }
        public int productQuantity { get; set; }
        public int order { get; set; }
    }
}
