namespace EMR_AIPredictionSystem.Model.Request.MedicalFile
{
    public class GetMedicalFilePagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; }
        public int? YearOfFile { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
    }
}
