namespace RecipeApp.Models
{
    public class CurrencyViewModel
    {
        public string SellCurrency { get; set; } = "USD";
        public string BuyCurrency { get; set; } = "UAH";
        public decimal Amount { get; set; }
        public decimal? ResultAmount { get; set; }
    }
}