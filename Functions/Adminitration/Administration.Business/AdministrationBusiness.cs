using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.BotWsp;
using TC.Connectors.BotWsp.SendMessageWsp;
using TC.Connectors.JwtAuth;
using TC.Connectors.JwtAuth.RegisterUser;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.Administration.Business.Models;

namespace TC.Functions.Administration.Business
{
    public class AdministrationBusiness : BaseBusiness<User, AdministrationContext>, IAdministrationBusiness
    {
        IMapper mapper;
        IJwtAuthManager serviceJwt;
        IBotWspManager serviceBot;
        bool isSentNotification;
        string messageNotification;

        public AdministrationBusiness(AdministrationContext context, IPrincipal userInfo, IConfiguration configuration, IJwtAuthManager serviceJwt, IBotWspManager serviceBot)
            : base(context, userInfo, configuration)
        {
            this.serviceJwt = serviceJwt;
            this.serviceBot = serviceBot;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, GetUserResult>()
                    .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.UserDetail.Name} {s.UserDetail.FirstLastName} {s.UserDetail.SecondLastName}"));

                cfg.CreateMap<GetUserDto, RegisterUserRequest>()
                  .ForMember(d => d.User, o => o.MapFrom(s => "ADMIN"));
            });
            mapper = new Mapper(config);
        }

        public Result<string> CreateUser(GetUserDto dto)
        {
            var createUserConector = serviceJwt.RegisterUser(mapper.Map<RegisterUserRequest>(dto));
            if(!createUserConector.Header.IsOk)
            {
                return Result<string>.SetError("Error en el conector");
            }
            if(!createUserConector.Body.IsOk)
            {
                return Result<string>.SetError("Error al momento de crear el usuario contactarse con el administrador");
            }
            if(dto.NotifyUser)
            {
                isSentNotification = SendNotification(dto);
                messageNotification = $"Notifiacion {(isSentNotification ? "enviada" : "no enviado")} correctamente";
            }
            return Result<string>.SetOk($"Usuario {dto.AccessNumber} creado con éxito, {messageNotification}");
        }

        private bool SendNotification(GetUserDto dto)
        {
            var paramater = Context.Parameters.Where(x => x.Code == "" && x.Groups == "").FirstOrDefault();
            paramater.Description = paramater.Description.Replace("<User>", $"{dto.FirstLastName} {dto.Name} {dto.SecondLastName}");
            var notifyUser = serviceBot.SendMessageWsp(new SendMessageWspRequest
            {
                Uid = $"{DateTime.Now.ToString("yyyymmddhhmmss")}{dto.AccessNumber}",
                To = $"591{dto.PhoneNumber}",
                Text = paramater.Description,
            });
            if (!notifyUser.Header.IsOk)
            {
                return false;
                
            }
            if (notifyUser.Body == null)
            {
                return false;
            }
            return true;
        }

        public Result<List<GetUserResult>> GetListUsers(GetUserDto dto)
        {
            var listUsers = Context.Users.ToList();
            if (dto.State != string.Empty || dto.State != null)
            {
                listUsers = listUsers.Where(x => x.State == dto.State).ToList();
            }
            if (dto.AccessNumber != string.Empty || dto.AccessNumber != null)
            {
                listUsers = listUsers.Where(x => x.UserName.Contains(dto.AccessNumber)).ToList();
            }
            return listUsers.Any() ? Result<List<GetUserResult>>.SetOk(mapper.Map<List<GetUserResult>>(listUsers)) :
                Result<List<GetUserResult>>.SetError("No existen Usuarios Registrados");
        }

        private User GetUser(string userName) => Context.Users.Where(x => x.UserName.Contains(userName)).FirstOrDefault();

        public Result<GetUserResult> GetUserId(GetUserDto dto)
        {
            var user = GetUser(dto.AccessNumber);
            return user == null ? Result<GetUserResult>.SetOk(mapper.Map<GetUserResult>(user))
                : Result<GetUserResult>.SetError($"El usuario {dto.AccessNumber} no existe");
        }


        public Result<string> ChangeStateUser(GetUserDto dto)
        {
            var user = GetUser(dto.AccessNumber);
            if (user == null)
            {
                Result<string>.SetError($"El usuario {dto.AccessNumber} no existe");
            }
            user.State = dto.State;
            user.DateModification = DateTime.Now;
            user.DateLastPasswordChange = DateTime.Now;
            Context.Save(user);
            return Result<string>.SetOk($"El usuario {dto.AccessNumber} fue desbloqueado con éxito");
        }

        public Result<string> SendDatesUser()
        {

            return null;
        }
    }
}
