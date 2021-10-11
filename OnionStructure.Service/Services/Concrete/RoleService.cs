using OnionStructure.Domain.Models;
using OnionStructure.Repositories.Repository.Abstract;
using OnionStructure.Service.Services.Abstract;
using OnionStructure.Contract.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionStructure.Service.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private IAppRepository<ApplicationUser,string> _userRepository;
        private IAppRepository<ApplicationRole, string> _roleRepository;

        public RoleService(IAppRepository<ApplicationUser, string> userRepository,
                                     IAppRepository<ApplicationRole, string> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<List<UserRoleDto>> GetAllUserRoles(string userId)
        {
            var list = await _userRepository.Find(x => x.Id == userId, x => x.UserRoles);
            var userRoles = list.UserRoles.ToList();
            var roles = await _roleRepository.GetAll();

            var userRoleList = from ur in userRoles
                               join r in roles on ur.RoleId equals r.Id
                               select new UserRoleDto
                               {
                                   RoleId = r.Id,
                                   RoleName = r.Name
                               };
            
            return userRoleList.ToList();

        }

        public Task<ApplicationRole> GetRole(string roleId)
        {
            return _roleRepository.Get(roleId);
        }

        public async Task<List<ApplicationRoleDto>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAll();
            return (from r in roles
                    select new ApplicationRoleDto
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    }).ToList();
        }
    }
}
