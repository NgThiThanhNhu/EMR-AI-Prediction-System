using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Response.Patient;

namespace EMR_AIPredictionSystem.Model.Response.MedicalFiles
{
    public class MedicalFileResponse
    {
        //Trả ra thông tin của MedicalFile
        public string Id { get; set; } = null!;
        public string Status { get; set; }
        public string? MedicalCategoryName { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        //Trả ra thông tin Patient và person
        public PatientResponse CurrentPatient { get; set; }
        //Trả ra gồm các DocumentType: chứa list MedicalDocument kèm theo MedicalRecord và Xét ngiệm ở mỗi Type
        public List<DocumentTypeDetailResponse> DocumentTypes { get; set; }

    }
}
