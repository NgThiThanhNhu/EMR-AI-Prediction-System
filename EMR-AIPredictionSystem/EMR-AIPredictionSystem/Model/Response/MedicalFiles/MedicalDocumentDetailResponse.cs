using EMR_AIPredictionSystem.Model.Response.MedicalRecord;

namespace EMR_AIPredictionSystem.Model.Response.MedicalFiles
{
    public class MedicalDocumentDetailResponse
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }   
        public string DocumentTypeName { get; set; }   // IUI, XET_NGHIEM, DIEN_TIM...
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MedicalRecordResponse> MedicalRecords { get; set; }
    }
}
