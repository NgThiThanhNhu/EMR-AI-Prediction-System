namespace EMR_AIPredictionSystem.Model.Request.MedicalFile
{
    public class CreateMedicalFileRequest
    {
        public string PatientId { get; set; }
        public int Year { get; set; }
        public string FileCategoryId { get; set; }
        public Guid CreatedBy { get; set; }//Id user người tạo
    }
}
