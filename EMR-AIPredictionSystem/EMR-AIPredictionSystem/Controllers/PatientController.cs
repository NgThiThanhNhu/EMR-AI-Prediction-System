using Azure.Core;
using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Request.User;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;
using EMR_AIPredictionSystem.Model.Response.Patient;
using EMR_AIPredictionSystem.Model.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EMR_AIPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        //[GET] /admin/all-patients
        [HttpGet("admin/all-patients")]
        public async Task<BaseResponse<List<PatientPagingResponse>>> GetPatientsPage([FromQuery] GetPatientPagingRequest request)
        {
            BaseResponse<List<PatientPagingResponse>> baseResponse = await _patientService.GetPatientsPage(request);
            return baseResponse;
        }

        //[POST] /admin/create-patient
        [HttpPost("create-patient")]
        public async Task<BaseResponse<PatientResponse>> CreateNewPatient([FromBody] CreatePatientRequest request)
        {
            BaseResponse<PatientResponse> baseResponse = await _patientService.CreateNewPatient(request);
            return baseResponse;
        }
        //[PUT] /api/class/id
        [HttpPut("update-patient/{id}")]
        public async Task<BaseResponse<PatientResponse>> UpdateExistPatient(string id, [FromBody] UpdatePatientRequest request)
        {
            BaseResponse<PatientResponse> baseResponse = await _patientService.UpdateExistPatient(id,request);
            return baseResponse;
        }

        // [DELETE] /api/class/id
        [HttpDelete("delete-patient/{id}")]
        public async Task<BaseResponse<PatientResponse>> DeleteExistPatient(string id)
        {
            BaseResponse<PatientResponse> baseResponse = await _patientService.DeleteExistPatient(id);
            return baseResponse;
        }

    }

}
