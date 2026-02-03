using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Request.MedicalFile;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using EMR_AIPredictionSystem.Model.Response.MedicalRecord;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalFileController : Controller
    {
        private readonly IMedicalFileService _medicalFileService;

        public MedicalFileController(IMedicalFileService medicalFileService)
        {
            _medicalFileService = medicalFileService;
        }

        //[GET] /admin/all-patients
        [HttpGet("medical-records")]
        public async Task<BaseResponse<List<MedicalFilePagingResponse>>> GetMedicalFilesPage([FromQuery] GetMedicalFilePagingRequest request)
        {
            BaseResponse<List<MedicalFilePagingResponse>> baseResponse = await _medicalFileService.GetMedicalFilesPage(request);
            return baseResponse;
        }

        //[GET] /medical-file/id
        [HttpGet("medical-file/{id}")]
        public async Task<BaseResponse<MedicalFileResponse>> GetMedicalFileId(string id)
        {
            BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.GetMedicalFileId(id);
            return baseResponse;
        }

        //[POST] /create-medicalFile
        [HttpPost("create-medicalFile")]
        public async Task<BaseResponse<MedicalFileResponse>> CreateNewMedicalFile([FromBody] CreateMedicalFileRequest request)
        {
            BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.CreateNewMedicalFile(request);
            return baseResponse;
        }
        //[PUT] /update-medicalFile/id
        [HttpPut("update-medicalFile/{id}")]
        public async Task<BaseResponse<MedicalFileResponse>> UpdateExistMedicalFile(string id, [FromBody] UpdateMedicalFileRequest request)
        {
            BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.UpdateExistMedicalFile(id, request);
            return baseResponse;
        }

        // [DELETE] /delete-medicalFile/id
        [HttpDelete("delete-medicalFile/{id}")]
        public async Task<BaseResponse<MedicalFileResponse>> DeleteExistMedicalFile(string id)
        {
            BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.DeleteExistMedicalFile(id);
            return baseResponse;
        }
    }
}
