﻿using AutoMapper.Internal.Mappers;
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

        public async Task<UsersModal> ForgotPass(string email, string password, int orgId)
        {
            UsersModal usersModal = new UsersModal();
            var Forgotpassword = await _AuthenticationRepository.ForgotPass(email, password, orgId);
            usersModal = ObjectMapper.Mapper.Map<UsersModal>(Forgotpassword);
            return usersModal;
        }

        public async Task<UsersModal> Get_Exist_Login_Data(string email, string phone, int orgId)
        {
            UsersModal users = new UsersModal();
            var UserLogin = await _AuthenticationRepository.Get_Exist_Login_Data(email, phone);
            if(UserLogin != null)
            {
                var ExistsUser = await _AuthenticationRepository.AddExistsUser(UserLogin, orgId);
                await _AuthenticationRepository.Get_Exist_UserProfile_Data(ExistsUser, orgId);
                users = ObjectMapper.Mapper.Map<UsersModal>(UserLogin);
            }
            
            return users;
        }

        public async Task<UsersModal> Get_Login_Data(string email, string passWord, int orgid)
        {
            UsersModal users = new UsersModal();
            users.LastLoginDate = DateTime.Now;
            var UserLogin = await _AuthenticationRepository.GetAuthenticato_Details(email, passWord, orgid);
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

        public async Task<UsersModal> RegisterDetail(string userName, string password, string email, int orgId)
        {
            UsersModal usersModal = new UsersModal();
            var RegData =  await _AuthenticationRepository.RegisterDetail(userName, password, email, orgId);
            usersModal = ObjectMapper.Mapper.Map<UsersModal>(RegData);
            return usersModal;
        }
    }
    
}
