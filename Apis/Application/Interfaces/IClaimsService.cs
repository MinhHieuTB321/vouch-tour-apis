namespace Application.Interfaces
{
    public interface IClaimsService
    {
        public Guid GetId { get; }

        public Guid GetUserRoleId { get; }

        public string GetEmail { get; }
    }
}
