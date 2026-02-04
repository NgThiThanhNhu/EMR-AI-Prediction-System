using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.DigitalSignature;

namespace EMR_AIPredictionSystem.IService
{
    public interface IDigitalSignatureService
    {
        Task<BaseResponse<List<GetAllDigitalSignatureResponse>>> GetDigitalSignatures();
    }
}
