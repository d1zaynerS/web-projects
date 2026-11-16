namespace RecipeApp.Models
{
    public class IngredientViewModel
    {
        public string Name { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }

    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<IngredientViewModel> Ingredients { get; set; } = new();
    }
}