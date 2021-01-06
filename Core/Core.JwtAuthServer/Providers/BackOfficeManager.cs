namespace TC.Core.JwtAuthServer.Providers
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Configuration;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.Owin.Security;

    using AutoMapper;
    using TC.Core.JwtAuthServer.Formats;
    using TC.Core.JwtAuthServer.Models;


    public class BackOfficeManager
    {
        //ISegurinetManager segurinetManager;
        IMapper mapper;

        public BackOfficeManager()
        {
            //segurinetManager = new SegurinetManager();

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<ValidateUserResponse, UserBackOfficeModel>()
                //.ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles));
            });
            mapper = new Mapper(config);
        }

        internal Result<ClaimsIdentity> GetClaimsUser(string userName, string password)
        {
            //var request = new ValidateUserRequest
            //{
            //    UserName = userName,
            //    Password = password
            //};
            //var user = segurinetManager.ValidateUser(request);
            //if (user.Header.IsOk)
            //{
            //    if (user.Body.Status == AuthenticationStatus.OK)
            //    {
            //        var userBO = mapper.Map<UserBackOfficeModel>(user.Body);
            //        userBO.UserName = userName;
            //        return Result<ClaimsIdentity>.SetOk(GetClaimsIdentity(userBO));
            //    }
            //    else
            //    {
            //        string messageError = string.Empty;
            //        switch (user.Body.Status)
            //        {
            //            case AuthenticationStatus.PasswordExpired:
            //            case AuthenticationStatus.UserMustChangePassword:
            //                messageError = "El usuario debe cambiar el password";
            //                break;
            //            case AuthenticationStatus.NotAuthenticated:
            //                if (String.IsNullOrEmpty(user.Body.ExceptionMessage))
            //                    messageError = "El usuario no existe";
            //                else
            //                    messageError = user.Body.ExceptionMessage;
            //                break;
            //            case AuthenticationStatus.MaxSessionsReached:
            //                messageError = "Se ha alcanzado el nùmero máximo de sesiones del aplicativo";
            //                break;
            //            default:
            //                messageError = user.Body.ExceptionMessage;
            //                break;
            //        }
            //        return Result<ClaimsIdentity>.SetError(messageError);
            //    }
            //}
            return null;
        }

        private ClaimsIdentity GetClaimsIdentity(UserBackOfficeModel userBO)
        {
            return new ClaimsIdentity(new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, userBO.UserName),
                new Claim(ClaimTypes.Name,userBO.UserName),
                new Claim("user_name", string.Format("{0} {1} {2}",userBO.LastName, userBO.MotherLastName, userBO.Name)),
                new Claim("roles", string.Join(",", userBO.Roles)),
                new Claim("policies", string.Join(",", userBO.Policies)),
                /*new Claim("state", userBO.Status.ToString())*/}, "BackAuthType");
        }
    }
}