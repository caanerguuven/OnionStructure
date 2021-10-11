using OnionStructure.Domain.Models.Base;
using System.Collections.Generic;

namespace OnionStructure.Domain.Models
{
    public class ApplicationRole : IEntity<string>
    {
        public string Id { get; set; }
        public virtual string Name { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
