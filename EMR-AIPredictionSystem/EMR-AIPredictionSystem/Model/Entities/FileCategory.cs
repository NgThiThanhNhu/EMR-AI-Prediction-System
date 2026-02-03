namespace EMR_AIPredictionSystem.Model.Entities
{
    public class FileCategory
    {
        public string Id { get; set; } = null!;     
        public string Name { get; set; } = null!;
        public ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();
    }
}
