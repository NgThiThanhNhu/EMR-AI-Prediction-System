using EMR_AIPredictionSystem.Model.Request.MedicalFile;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using EMR_AIPredictionSystem.Model.Response.MedicalRecord;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.IService
{
    public interface IMedicalFileService  
    {
        Task<BaseResponse<List<MedicalFilePagingResponse>>> GetMedicalFilesPage(GetMedicalFilePagingRequest request);
        Task<BaseResponse<MedicalFileResponse>> GetMedicalFileId(string id);
        Task<BaseResponse<MedicalFileResponse>> CreateNewMedicalFile(CreateMedicalFileRequest request);
        Task<BaseResponse<MedicalFileResponse>> UpdateExistMedicalFile(string id, UpdateMedicalFileRequest request);
        Task<BaseResponse<MedicalFileResponse>> DeleteExistMedicalFile(string id);
    }
}
