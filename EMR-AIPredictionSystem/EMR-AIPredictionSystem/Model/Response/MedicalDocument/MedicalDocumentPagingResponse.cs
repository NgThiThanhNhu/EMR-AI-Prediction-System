using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;
using EMR_AIPredictionSystem.Model.Response.MedicalRecord;
using EMR_AIPredictionSystem.Model.Response.Patient;

namespace EMR_AIPredictionSystem.Model.Response.MedicalDocument
{
    public class MedicalDocumentPagingResponse
    {
        public int STT { get; set; }
        public string DocumentId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string MedicalFileName { get; set; }
        public int SignaturedQuantity { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public PatientResponse Patient { get; set; }
        public List<DigitalSignatureResponse> DigitalSignatures { get; set; }
        public MedicalRecordResponse MedicalRecords { get; set; }
    }
}
