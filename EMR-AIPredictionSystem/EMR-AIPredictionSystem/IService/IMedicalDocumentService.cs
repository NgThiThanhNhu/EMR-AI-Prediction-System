using EMR_AIPredictionSystem.Model.Request.MedicalDocument;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.MedicalDocument;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.IService
{
    public interface IMedicalDocumentService
    {
        Task<BaseResponse<List<MedicalDocumentPagingResponse>>> GetMedicalDocumentPage(MedicalDocumentPagingRequest request);
        Task<BaseResponse<MedicalDocumentResponse>> GetMedicalDocumentById(string id);
        Task<BaseResponse<MedicalDocumentResponse>> UpdateExisMedicalDocument(string id, UpdateMedicalDocumentRequest request);
    }
}
