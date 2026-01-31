using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.Patient
{
    public class PatientResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? AvatarPath { get; set; }
    }
}
