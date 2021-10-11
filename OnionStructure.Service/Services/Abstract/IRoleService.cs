using OnionStructure.Domain.Models;
using OnionStructure.Contract.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionStructure.Service.Services.Abstract
{
    public interface IRoleService
    {
        Task<List<UserRoleDto>> GetAllUserRoles(string userId);
        Task<ApplicationRole> GetRole(string roleId);
        Task<List<ApplicationRoleDto>> GetAllRoles();
    }
}
