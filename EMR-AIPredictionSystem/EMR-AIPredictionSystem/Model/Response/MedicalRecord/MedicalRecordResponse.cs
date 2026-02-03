using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Response.Appointment;

namespace EMR_AIPredictionSystem.Model.Response.MedicalRecord
{
    public class MedicalRecordResponse
    {
        public string Id { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public AppointmentResponse AppointmentDetail { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string? ClinicalNotes { get; set; }
        public string TreatmentStatus { get; set; }
        public string? RecordStatus { get; set; }
        public DateTime? VisitAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaidAt { get; set; }
        public string PaymentConfirmedByName { get; set; }
        public ClinicalVitalResponse ClinicalVitals { get; set; }// chứa 1 phiếu lâm sàng
        public List<LabResultResponse> LabResults { get; set; } //chứa N kết quả xét nghiệm
    }
}
