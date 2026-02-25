using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class RoleClaim : Entity
    {
        public string Type { get; private set; } = string.Empty;

        public string Value { get; private set; } = string.Empty;

        public Guid RoleId { get; private set; }

        public Role? Role { get; private set; }

        protected RoleClaim() { }

        public void SetRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);

            Role = role;
        }

        public static RoleClaim Create(string type, string value)
        {
            var roleClaim = new RoleClaim
            {
                Id = Guid.NewGuid(),
                Type = type,
                Value = value
            };

            return roleClaim;
        }
    }
}
