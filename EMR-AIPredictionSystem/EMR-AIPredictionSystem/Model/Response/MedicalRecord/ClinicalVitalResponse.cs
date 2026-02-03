namespace EMR_AIPredictionSystem.Model.Response.MedicalRecord
{
    public class ClinicalVitalResponse
    {
        public string BloodPressure { get; set; }
        public int? HeartRate { get; set; }
        public float? Temperature { get; set; }
        public int? SpO2 { get; set; }
        public float? Weight { get; set; }
        public int? Height { get; set; }
        public DateTime? RecordedAt { get; set; }
    }
}
