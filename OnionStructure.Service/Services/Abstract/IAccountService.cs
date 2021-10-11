using OnionStructure.API.ViewModels.Response;
using OnionStructure.Domain.Models;
using OnionStructure.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnionStructure.Service.Services.Abstract
{
    public interface IAccountService
    {
        Task<AccountLoginResponseDto> Login(AccountLoginDto model);
        Task<List<ApplicationUser>> GetAllAccount();
        Task<ApplicationUser> GetAccount(string id);
        Task<ApplicationUser> FindAccount(Expression<Func<ApplicationUser, bool>> predicate);
        Task InsertAccount(ApplicationUser entity);
        Task UpdateAccount(ApplicationUser entity);
        Task DeleteAccount(string id);
    }
}
