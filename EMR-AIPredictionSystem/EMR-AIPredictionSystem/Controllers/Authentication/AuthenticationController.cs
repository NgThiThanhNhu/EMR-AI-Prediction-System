using EMR_AIPredictionSystem.IService.Authentication;
using EMR_AIPredictionSystem.Model.Request.Authentication;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMR_AIPredictionSystem.Controllers.Authentication
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            BaseResponse<LoginResponse> baseResponse = await _authenticationService.Login(loginRequest);
            return baseResponse;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<BaseResponse<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            BaseResponse<RegisterResponse> baseResponse = await _authenticationService.Register(registerRequest);
            return baseResponse;
        }
        [AllowAnonymous]
        [HttpPost("ConfirmOtp")]
        public async Task<BaseResponse<OtpResponse>> ConfirmOTP(OtpRequest otpRequest)
        {
            BaseResponse<OtpResponse> baseResponse = await _authenticationService.ConfirmOTP(otpRequest);
            return baseResponse;
        }
        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<BaseResponse<LogoutResponse>> Logout()
        {
            BaseResponse<LogoutResponse> baseResponse = await _authenticationService.Logout();
            return baseResponse;

        }

    }
}
