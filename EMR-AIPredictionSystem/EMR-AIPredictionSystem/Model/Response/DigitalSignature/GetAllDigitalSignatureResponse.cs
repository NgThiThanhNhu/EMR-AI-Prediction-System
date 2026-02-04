namespace EMR_AIPredictionSystem.Model.Response.DigitalSignature
{
    public class GetAllDigitalSignatureResponse
    {
        public string Id { get; set; } = null!;

        public string DocumentId { get; set; } = null!;

        public Guid SignedBy { get; set; }
        public string SignedByName { get; set; }

        public DateTime? SignedAt { get; set; }

        public string? SignaturePath { get; set; }

        public int? SignOrder { get; set; }
    }
}
