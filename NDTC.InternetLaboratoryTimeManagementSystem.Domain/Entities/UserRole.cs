using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class UserRole
    {
        public Guid UserId { get; private set; }

        public User? User { get; set; }

        public Guid RoleId { get; private set; }

        public Role? Role { get; set; }

        public static UserRole Create(User user, Role role)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(role);

            var userRole = new UserRole
            {
                Role = role,
                User = user
            };

            return userRole;
        }
    }
}
