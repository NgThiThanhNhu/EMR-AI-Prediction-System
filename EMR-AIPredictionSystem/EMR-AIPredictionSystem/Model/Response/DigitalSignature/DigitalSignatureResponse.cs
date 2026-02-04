namespace EMR_AIPredictionSystem.Model.Response.DigitalSignature
{
    public class DigitalSignatureResponse
    {
        public string Id { get; set; } = null!;

        public Guid SignedBy { get; set; }
        public string SignedByName { get; set; }

        public DateTime? SignedAt { get; set; }

        public string? SignaturePath { get; set; }

        public int? SignOrder { get; set; }
    }
}
