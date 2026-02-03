using EMR_AIPredictionSystem.Model.Request.User;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;
using EMR_AIPredictionSystem.Model.Response.Patient;
using EMR_AIPredictionSystem.Model.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.IService
{
    public interface IPatientService
    {
        Task<BaseResponse<List<PatientPagingResponse>>> GetPatientsPage(GetPatientPagingRequest request);
        Task<BaseResponse<PatientResponse>> GetPatientById(string id);
        Task<BaseResponse<PatientResponse>> CreateNewPatient(CreatePatientRequest request);
        Task<BaseResponse<PatientResponse>> UpdateExistPatient(string id, UpdatePatientRequest request);
        Task<BaseResponse<PatientResponse>> DeleteExistPatient(string id);
    }
}
