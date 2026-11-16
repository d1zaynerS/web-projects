namespace RecipeApp.Models
{
    public enum SubjectType
    {
        GeneralQuery,
        TechnicalIssue,
        Cooperation
    }

    public class FeedbackViewModel
    {
        public SubjectType Subject { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; }
    }
}