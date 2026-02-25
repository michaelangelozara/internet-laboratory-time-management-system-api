using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class Role : Entity
    {
        public string Name { get; private set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; } = [];

        public ICollection<RoleClaim> RoleClaims { get; } = [];

        protected Role() { }

        public static Role Create(string name)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            return role;
        }

        public void AddRoleClaims(params RoleClaim[] roleClaims)
        {
            if (roleClaims.Length > 0)
            {
                foreach (var roleClaim in roleClaims)
                {
                    if (roleClaim == null)
                        throw new ArgumentNullException($"Role claim cannot be null while associating to {Name} role.");

                    RoleClaims.Add(roleClaim);
                }
            }
        }
    }
}
