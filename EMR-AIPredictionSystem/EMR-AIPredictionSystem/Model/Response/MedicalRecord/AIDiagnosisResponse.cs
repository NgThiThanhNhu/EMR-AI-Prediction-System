namespace EMR_AIPredictionSystem.Model.Response.MedicalRecord
{
    public class AIDiagnosisResponse
    {
        public string PredictedDisease { get; set; }
        public float ConfidenceScore { get; set; }
        public string SuggestedTreatment { get; set; }
        public string Priority { get; set; }

    }
}
