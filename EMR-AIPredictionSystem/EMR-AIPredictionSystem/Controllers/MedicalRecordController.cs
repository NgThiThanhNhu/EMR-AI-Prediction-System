using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Request.User;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Patient;
using EMR_AIPredictionSystem.Model.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        //[GET] /admin/all-patients
        //[HttpGet("medical-records")]
        //public async Task<BaseResponse<List<PatientPagingResponse>>> GetPatientsPage([FromQuery] GetPatientPagingRequest request)
        //{
        //    BaseResponse<List<PatientPagingResponse>> baseResponse = await _patientService.GetPatientsPage(request);
        //    return baseResponse;
        //}

        //[GET] /medical-record/id
        //[HttpGet("medical-record/{id}")]
        //public async Task<BaseResponse<PatientResponse>> GetPatientById(string id)
        //{
        //    BaseResponse<PatientResponse> baseResponse = await _patientService.GetPatientById(id);
        //    return baseResponse;
        //}

        ////[POST] /admin/create-patient
        //[HttpPost("create-patient")]
        //public async Task<BaseResponse<PatientResponse>> CreateNewPatient([FromBody] CreatePatientRequest request)
        //{
        //    BaseResponse<PatientResponse> baseResponse = await _patientService.CreateNewPatient(request);
        //    return baseResponse;
        //}
        ////[PUT] /update-patient/id
        //[HttpPut("update-patient/{id}")]
        //public async Task<BaseResponse<PatientResponse>> UpdateExistPatient(string id, [FromBody] UpdatePatientRequest request)
        //{
        //    BaseResponse<PatientResponse> baseResponse = await _patientService.UpdateExistPatient(id, request);
        //    return baseResponse;
        //}

        //// [DELETE] /delete-patient/id
        //[HttpDelete("delete-patient/{id}")]
        //public async Task<BaseResponse<PatientResponse>> DeleteExistPatient(string id)
        //{
        //    BaseResponse<PatientResponse> baseResponse = await _patientService.DeleteExistPatient(id);
        //    return baseResponse;
        //}
    }
}
