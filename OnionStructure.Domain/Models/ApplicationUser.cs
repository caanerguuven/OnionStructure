using OnionStructure.Domain.Models.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnionStructure.Domain.Models
{
    public class ApplicationUser : IEntity<string>
    {
        public string Id { get; set; }
        public int CasinoID { get; set; }
        public string Email { get; set; }
        [PersonalData]
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        [PersonalData]
        public bool PhoneNumberConfirmed { get; set; }
        [PersonalData]
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [ProtectedPersonalData]
        public string UserName { get; set; }
        public int VisibilityGroupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ext_clerk_id { get; set; }
        public int? LastLocationID { get; set; }
        public bool? Hidden { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public bool PasswordResetRequired { get; set; }
        public string EmployeeBadgeId { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
