namespace AlgorithmAppProduction.Classes
{
    public class ProductionTreeElement
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public ProductionTreeElementRecipe Recipe { get; set; }
        public bool Done { get; set; } = false;
    }

    public class ProductionTreeElementRecipe
    {
        public ProductionTreeElement FirstComponent { get; set; }
        public ProductionTreeElement SecondComponent { get; set; }
        public int? OperationId { get; set; }
    }
}