namespace EMR_AIPredictionSystem.Model.Request.MedicalDocument
{
    public class MedicalDocumentPagingRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; }
        public string? FileCategoryId { get; set; } // Id loại hồ sơ
        public Guid? SignaturedBy { get; set; } //Người ký
        public DateTime? CreatedAt { get; set; }
    }
}
