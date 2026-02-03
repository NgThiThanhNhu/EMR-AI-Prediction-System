using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.MedicalFiles
{
    public class DocumentTypeDetailResponse
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int? MaxSignatures { get; set; }
        //Chứa các MedicalDocument thuộc type document này
        public List<MedicalDocumentDetailResponse> MedicalDocuments { get; set; }//danh sách các phiếu thuộc type document(lâm sàng, xét nghiệm,...)
    }
}
