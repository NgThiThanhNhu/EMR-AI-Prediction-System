namespace EMR_AIPredictionSystem.Model.Response.MedicalRecord
{
    public class LabResultResponse
    {
        public string TestName { get; set; }
        public string ResultValue { get; set; }
        public string Unit { get; set; }
        public string ReferenceRange { get; set; }
        public string Evaluation { get; set; }
    }
}
