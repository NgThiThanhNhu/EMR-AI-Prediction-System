using EMR_AIPredictionSystem.IService;
using EMR_AIPredictionSystem.Model.Request.MedicalDocument;
using EMR_AIPredictionSystem.Model.Request.MedicalFile;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;
using EMR_AIPredictionSystem.Model.Response.MedicalDocument;
using EMR_AIPredictionSystem.Model.Response.MedicalFiles;
using Microsoft.AspNetCore.Mvc;

namespace EMR_AIPredictionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalDocumentController : Controller
    {
        private readonly IMedicalDocumentService _medicalDocumentService;

        public MedicalDocumentController(IMedicalDocumentService medicalDocumentService)
        {
            _medicalDocumentService = medicalDocumentService;  
        }

        //[GET] /all-documents
        [HttpGet("all-documents")]
        public async Task<BaseResponse<List<MedicalDocumentPagingResponse>>> GetMedicalDocumentPage([FromQuery] MedicalDocumentPagingRequest request)
        {
            BaseResponse<List<MedicalDocumentPagingResponse>> baseResponse = await _medicalDocumentService.GetMedicalDocumentPage(request);
            return baseResponse;
        }

        //[GET] /document/id
        [HttpGet("document/{id}")]
        public async Task<BaseResponse<MedicalDocumentResponse>> GetMedicalDocumentById(string id)
        {
            BaseResponse<MedicalDocumentResponse> baseResponse = await _medicalDocumentService.GetMedicalDocumentById(id);
            return baseResponse;
        }

        ////[POST] /create-medicalFile
        //[HttpPost("create-medicalFile")]
        //public async Task<BaseResponse<MedicalFileResponse>> CreateNewMedicalFile([FromBody] CreateMedicalFileRequest request)
        //{
        //    BaseResponse<MedicalFileResponse> baseResponse = await _medicalFileService.CreateNewMedicalFile(request);
        //    return baseResponse;
        //}
        //[PUT] /update-document/id
        [HttpPut("update-document/{id}")]
        public async Task<BaseResponse<MedicalDocumentResponse>> UpdateExisMedicalDocument(string id, [FromBody] UpdateMedicalDocumentRequest request)
        {
            BaseResponse<MedicalDocumentResponse> baseResponse = await _medicalDocumentService.UpdateExisMedicalDocument(id, request);
            return baseResponse;
        }
    }
}
