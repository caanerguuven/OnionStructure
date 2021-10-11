using OnionStructure.Domain.Models.Base;

namespace OnionStructure.Domain.Models
{
    public class ApplicationUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
