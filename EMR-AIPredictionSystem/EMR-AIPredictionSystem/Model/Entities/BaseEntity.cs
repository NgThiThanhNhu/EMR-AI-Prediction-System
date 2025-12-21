namespace EMR_AIPredictionSystem.Model.Entities
{
    public class BaseEntity
    {
        public string? CreateUser { get; set; } //người tạo
        public string? UpdateUser { get; set; } //người sửa
        public string? DeleteUser { get; set; } //người xóa
        public DateTime CreateDate { get; set; } //ngày tạo
        public DateTime? UpdateDate { get; set; }//ngày sửa
        public DateTime? DeleteDate { get; set; }//ngày xóa
        public bool? IsDeleted { get; set; } = false;
    }
}
