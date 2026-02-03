using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.MedicalFiles
{
    public class MedicalFilePagingResponse
    {
        public int STT { get; set; }
        public string Id { get; set; } = null!;
        public string Status { get; set; }
        public string? MedicalCategoryName { get; set; }
        public int? Year { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string  CreatedByName { get; set; }
    }
}
