namespace EMR_AIPredictionSystem.Model.Entities.UserManage
{
    public class Role : BaseEntity
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationUser> applicationUsers { get; set; }
    }
}
