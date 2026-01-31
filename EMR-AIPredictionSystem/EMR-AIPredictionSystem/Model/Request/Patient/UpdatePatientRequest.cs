namespace EMR_AIPredictionSystem.Model.Request.User
{
    public class UpdatePatientRequest
    {
        public string? AvatarPath { get; set; }
        public string? FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
    }
}
