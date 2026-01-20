using EMR_AIPredictionSystem.Model.Request.Authentication;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;

namespace EMR_AIPredictionSystem.IService.Authentication
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<LoginResponse>> Login(LoginRequest loginRequest);
        Task<BaseResponse<RegisterResponse>> Register(RegisterRequest registerRequest);
        Task<BaseResponse<OtpResponse>> ConfirmOTP(OtpRequest otpRequest);
        Task<BaseResponse<LogoutResponse>> Logout();
    }
}
