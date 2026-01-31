using EMR_AIPredictionSystem.Common;
using EMR_AIPredictionSystem.Data;
using EMR_AIPredictionSystem.IService.Authentication;
using EMR_AIPredictionSystem.Model.Entities;
using EMR_AIPredictionSystem.Model.Request.Authentication;
using EMR_AIPredictionSystem.Model.Response;
using EMR_AIPredictionSystem.Model.Response.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EMR_AIPredictionSystem.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private static Dictionary<string, OtpData> otpStore = new();
        private readonly EmailSettings _emailSettings;
        public AuthenticationService(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IOptions<EmailSettings> options)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
            _emailSettings = options.Value;
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            BaseResponse<LoginResponse> response = new BaseResponse<LoginResponse>();
            User applicationUser = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsVerified == true && x.Email == loginRequest.Email);
            if (applicationUser == null || applicationUser.IsVerified == false)
            {
                response.IsSuccess = false;
                response.Message = "Tài khoản không tồn tại hoặc chưa được xác thực otp";
                return response;
            }
            string hashInputPassword = Encrypt_Decrypt.EncodePassword(loginRequest.Password, applicationUser.Salt);
            if (hashInputPassword != applicationUser.PasswordHash)
            {
                response.IsSuccess = false;
                response.Message = "Mật khẩu không chính xác";
                return response;
            }
            TokenRequest tokenRequest = new TokenRequest();
            tokenRequest.Id = applicationUser.Id;
            tokenRequest.Name = applicationUser.Username;
            tokenRequest.Rolename = applicationUser.Role.RoleName;
            string jwtToken = Encrypt_Decrypt.GenerateJwtToken(tokenRequest, _configuration);
            _contextAccessor.HttpContext.Response.Cookies.Append("jwtToken", jwtToken ?? "", new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.None,//vì fe và be chạy trên 2 domain khác nhau là 3000 và 7260, set có SameSite=Strict, điều này có thể khiến nó không được gửi khi truy cập từ một domain khác.
                Secure = true,
                Expires = DateTime.Now.AddDays(1)
            });
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Email = loginRequest.Email;
            loginResponse.Token = jwtToken;
            loginResponse.Rolename = applicationUser.Role.RoleName;
            response.IsSuccess = true;
            response.Message = "Đăng nhập thành công";
            response.data = loginResponse;
            return response;
        }

        public async Task<BaseResponse<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            BaseResponse<RegisterResponse> response = new BaseResponse<RegisterResponse>();
            User applicationUser = await _context.Users.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Email == registerRequest.Email);
            if (applicationUser != null)
            {
                response.IsSuccess = false;
                response.Message = "Đã có tài khoản";
                return response;
            }
            string salt = Encrypt_Decrypt.GenerateSalt();
            string hashPassword = Encrypt_Decrypt.EncodePassword(registerRequest.Password, salt);
            User user = new User();
            user.Username = registerRequest.UserName;
            user.Email = registerRequest.Email;
            user.PasswordHash = hashPassword;
            user.Salt = salt;
            user.CreateDate = DateTime.Now;
            Role role = _context.Roles.FirstOrDefault(x => x.RoleName == "Bệnh nhân" && x.IsDeleted == false);
            if (role is null)
            {
                role = new Role();
                //role.Id = Guid.NewGuid();
                role.Id = "RL000000003";
                role.RoleName = "Bệnh nhân";
                _context.Roles.Add(role);
            }
            user.RoleId = role.Id;
            RegisterResponse registerResponse = new RegisterResponse();
            registerResponse.Email = user.Email;
            string newOTP = GetOTP();
            if (!sendEmail(newOTP, user.Email))
            {
                response.IsSuccess = false;
                response.Message = "Email không hợp lệ";
                response.data = registerResponse;
                return response;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            OtpData data = new OtpData();
            data.Code = newOTP;
            data.ExpiredTime = DateTime.Now.AddMinutes(8);
            otpStore[user.Email] = data;
            response.IsSuccess = true;
            response.Message = "Kiểm tra email xác thực otp";
            response.data = registerResponse;
            return response;
        }
        private string GetOTP()
        {
            return new Random().Next(100000, 999999).ToString();
        }
        private string htmlEmail(string email, string otp)
        {
            return "Xin chào " + email + ", bạn đã đăng ký tài khoản khám chữa bệnh." +
            " Đây là mã xác nhận OTP là: " + otp;
        }
        private bool sendEmail(string otp, string email)
        {
            string body = htmlEmail(email, otp);
            string title = "Mã xác nhận OTP....";
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new SmtpClient();
                {
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    smtp.Host = _emailSettings.Host;
                    smtp.Port = _emailSettings.Port;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new NetworkCredential()
                    //{
                    //    UserName = ,
                    //    Password =
                    //};
                    smtp.Credentials = new NetworkCredential(
                         _emailSettings.Email,
                         _emailSettings.AppPassword
                     );
                }
                MailAddress fromAddress = new MailAddress("nguyenthithanhnhu163tp@gmail.com", "EMR-AIPrediction");
                message.From = fromAddress;
                message.To.Add(email);
                message.Subject = title;
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<BaseResponse<OtpResponse>> ConfirmOTP(OtpRequest otpRequest)
        {
            BaseResponse<OtpResponse> response = new BaseResponse<OtpResponse>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.IsVerified == false && x.Email == otpRequest.Email);
            if (!otpStore.ContainsKey(user.Email))
            {
                response.IsSuccess = false;
                response.Message = "OTP không tồn tại";
                response.data = new OtpResponse { isValidate = false };
                return response;
            }
            var stored = otpStore[user.Email];
            if (DateTime.Now > stored.ExpiredTime)
            {
                response.IsSuccess = false;
                response.Message = "Mã hết hiệu lực";
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                response.data = new OtpResponse { isValidate = false };
                return response;
            }
            if (stored.Code != otpRequest.Content)
            {
                response.IsSuccess = false;
                response.Message = "Mã OTP không chính xác";
                response.data = new OtpResponse { isValidate = false };
                return response;
            }
            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Lỗi hệ thống";
                response.data = new OtpResponse { isValidate = false };
                return response;
            }
            user.IsVerified = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            otpStore.Remove(otpRequest.Email);
            response.IsSuccess = true;
            response.Message = "Xác minh OTP thành công";
            response.data = new OtpResponse
            {
                isValidate = true,
                Email = otpRequest.Email,
            };
            return response;
        }

        public Task<BaseResponse<LogoutResponse>> Logout()
        {
            BaseResponse<LogoutResponse> response = new BaseResponse<LogoutResponse>();
            LogoutResponse logoutResponse = new LogoutResponse();
            logoutResponse.Name = getCurrentName();
            logoutResponse.RoleName = getCurrentRole();
            logoutResponse.Token = getCurrentToken();
            _contextAccessor.HttpContext.Response.Cookies.Delete("jwtToken");
            response.IsSuccess = true;
            response.Message = "Đăng xuất thành công";
            response.data = logoutResponse;
            return Task.FromResult(response);
        }
        private string getCurrentName()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }
        private string getCurrentRole()
        {
            return _contextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "RoleName")?.Value ?? "Unknown";
        }
        private string getCurrentToken()
        {
            return _contextAccessor.HttpContext?.Request?.Cookies["jwtToken"] ?? string.Empty;
        }
    }
}
