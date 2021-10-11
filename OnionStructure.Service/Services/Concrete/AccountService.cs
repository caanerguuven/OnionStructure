using OnionStructure.API.ViewModels.Response;
using OnionStructure.Domain.Models;
using OnionStructure.Repositories.Repository.Abstract;
using OnionStructure.Service.Services.Abstract;
using OnionStructure.Contract.DTOs;
using OnionStructure.Contract.Utils.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnionStructure.Service.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IAppRepository<ApplicationUser, string> _repository;
        private readonly IJwtUtils _jwtUtils;
        public AccountService(IAppRepository<ApplicationUser, string> repository,
                              IJwtUtils jwtUtils)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
        }

        public async Task<AccountLoginResponseDto> Login(AccountLoginDto model)
        {
            var userInfo = await FindAccount(x => x.UserName == model.UserName);
            if (userInfo is null)
            {
                throw new UnauthorizedAccessException("UserName not found");
            }

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var verifyResult = passwordHasher.VerifyHashedPassword(userInfo, userInfo.PasswordHash, model.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid Password");
            }

            var jwtToken = _jwtUtils.GenerateJwtToken(userInfo.Id);
            return new AccountLoginResponseDto
            {
                CasinoId = userInfo.CasinoID,
                Email = userInfo.Email,
                Id = userInfo.Id,
                Token = jwtToken,
                UserName = userInfo.UserName
            };
        }

        public async Task<ApplicationUser> FindAccount(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _repository.Find(predicate, x => x.UserRoles);
        }

        public async Task<ApplicationUser> GetAccount(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<ApplicationUser>> GetAllAccount()
        {
            return await _repository.GetAll();
        }

        public async Task InsertAccount(ApplicationUser entity)
        {
            await _repository.Insert(entity);
            await _repository.SaveChanges();
        }

        public async Task UpdateAccount(ApplicationUser entity)
        {
            _repository.Update(entity);
            await _repository.SaveChanges(); // in future, it can be moved to global transaction filter.
        }

        public async Task DeleteAccount(string id)
        {
            ApplicationUser user = await GetAccount(id);
            _repository.Delete(user);
            await _repository.SaveChanges();
        }


    }
}
