namespace EMR_AIPredictionSystem.Model.Response.DigitalSignature
{
    public class DigitalSignaturePagingResponse
    {
        public int STT { get; set; }
        public string SignatureId { get; set; } = null!;
        public string DocumentId { get; set; }
        public string SignerName { get; set; }
        public DateTime SignedAt { get; set; }
        public string DocumentName { get; set; }
        public string Status { get; set; }
    }
}
