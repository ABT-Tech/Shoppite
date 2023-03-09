using AutoMapper.Internal.Mappers;
using Shoppite.Application.Interfaces;
using Shoppite.Application.Mapper;
using Shoppite.Application.Models;
using Shoppite.Core.Interfaces;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Services
{
    public class AuthenticationsService : IAuthenticationsService
    {
        private readonly IAuthenticationsRepository _AuthenticationRepository;
        private readonly IAppLogger<AuthenticationsService> _logger;

        public AuthenticationsService(IAuthenticationsRepository AuthenticationRepository, IAppLogger<AuthenticationsService> logger)
        {
            _AuthenticationRepository = AuthenticationRepository ?? throw new ArgumentNullException(nameof(AuthenticationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<UsersModal> Get_Login_Data(string userName, string passWord, int orgid)
        {
            UsersModal users = new UsersModal();
            users.LastLoginDate = DateTime.Now;
            var UserLogin = await _AuthenticationRepository.GetAuthenticato_Details(userName, passWord, orgid);
            var logo = await _AuthenticationRepository.Get_Logo(orgid);
            users = ObjectMapper.Mapper.Map<UsersModal>(UserLogin);
            if (users == null)
            {
                users = new UsersModal();
            }
            users.LogoModel = ObjectMapper.Mapper.Map<LogoModel>(logo);
            return users;
        }

        public async Task<UsersModal> Get_Logo(int orgid)
        {
            UsersModal users = new UsersModal();
            var UserLogin = await _AuthenticationRepository.Get_Logo(orgid);
            users.LogoModel = ObjectMapper.Mapper.Map<LogoModel>(UserLogin);
            return users;
        }
    }
    
}
