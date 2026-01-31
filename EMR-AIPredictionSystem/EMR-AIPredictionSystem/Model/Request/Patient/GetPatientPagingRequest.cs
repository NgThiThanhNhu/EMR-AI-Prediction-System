namespace EMR_AIPredictionSystem.Model.Request.User
{
    public class GetPatientPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; }
        public int? YearOfBirth { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
    }
}
