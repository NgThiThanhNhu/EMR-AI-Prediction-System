using EMR_AIPredictionSystem.Model.Entities;

namespace EMR_AIPredictionSystem.Model.Response.User
{
    public class PatientPagingResponse
    {
        public int STT { get; set; }
        public string FullName { get; set; } = null!;//Họ tên
        public string Id { get; set; } = null!;//Mã BN
        public string Gender { get; set; }//Giới tính
        public DateOnly? DateOfBirth { get; set; }//Ngày sinh
        public string PhoneNumber { get; set; }//Số điện thoại
        public string? Province { get; set; }//Tỉnh
        public string? Ward { get; set; }//Phường/Xã
    }
}
