namespace EMR_AIPredictionSystem.Model.Entities.UserManage
{
    public class ApplicationUser : BaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Salt { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public bool isValidate { get; set; } = false;
    }
}
