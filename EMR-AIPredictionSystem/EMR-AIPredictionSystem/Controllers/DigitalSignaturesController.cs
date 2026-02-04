using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Request.MedicalFile;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalSignaturesController : Controller
    {
        private readonly IDigitalSignatureService _digitalSignatureService;

        public DigitalSignaturesController(IDigitalSignatureService digitalSignatureService)
        {
            _digitalSignatureService = digitalSignatureService;
        }

        //[GET] /admin/all-
        [HttpGet("all-signatures")]
        public async Task<BaseResponse<List<GetAllDigitalSignatureResponse>>> GetDigitalSignatures()
        {
            BaseResponse<List<GetAllDigitalSignatureResponse>> baseResponse = await _digitalSignatureService.GetDigitalSignatures();
            return baseResponse;
        }

        ////[GET] /medical-file/id
        //[HttpGet("medical-file/{id}")]
        //public async Task<BaseResponse<MedicalFileResponse>> GetMedicalFileId(string id)
        //{
        //    BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.GetMedicalFileId(id);
        //    return baseResponse;
        //}

        ////[POST] /create-medicalFile
        //[HttpPost("create-medicalFile")]
        //public async Task<BaseResponse<MedicalFileResponse>> CreateNewMedicalFile([FromBody] CreateMedicalFileRequest request)
        //{
        //    BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.CreateNewMedicalFile(request);
        //    return baseResponse;
        //}
        ////[PUT] /update-medicalFile/id
        //[HttpPut("update-medicalFile/{id}")]
        //public async Task<BaseResponse<MedicalFileResponse>> UpdateExistMedicalFile(string id, [FromBody] UpdateMedicalFileRequest request)
        //{
        //    BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.UpdateExistMedicalFile(id, request);
        //    return baseResponse;
        //}

        //// [DELETE] /delete-medicalFile/id
        //[HttpDelete("delete-medicalFile/{id}")]
        //public async Task<BaseResponse<MedicalFileResponse>> DeleteExistMedicalFile(string id)
        //{
        //    BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.DeleteExistMedicalFile(id);
        //    return baseResponse;
        //}
    }
}
