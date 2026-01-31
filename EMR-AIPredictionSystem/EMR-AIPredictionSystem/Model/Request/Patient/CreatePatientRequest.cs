namespace EMR_AIPredictionSystem.Model.Request.User
{
    public class CreatePatientRequest
    {
        public string? AvatarPath { get; set; }
        public string FullName { get; set; } = null!;
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
    }
}
