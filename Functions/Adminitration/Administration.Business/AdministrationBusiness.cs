﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        string messageNotification;
        int totalItems;
        bool isSentNotification;

        public AdministrationBusiness(AdministrationContext context, IPrincipal userInfo, IConfiguration configuration, IJwtAuthManager serviceJwt, IBotWspManager serviceBot)
            : base(context, userInfo, configuration)
        {
            this.serviceJwt = serviceJwt;
            this.serviceBot = serviceBot;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, string>()
                    .ConvertUsing(str => (str ?? "").Trim());

                cfg.CreateMap<User, GetUserResult>()
                    .ForMember(d => d.TotalItems, o => o.MapFrom(s => totalItems))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.UserDetail.Name))
                    .ForMember(d => d.FirstLastName, o => o.MapFrom(s => s.UserDetail.FirstLastName))
                    .ForMember(d => d.SecondLastName, o => o.MapFrom(s => s.UserDetail.SecondLastName))
                    .ForMember(d => d.UserRoles, o => o.MapFrom(s => s.UserRoles));

                cfg.CreateMap<UserRole, UserRoleResult>();

                cfg.CreateMap<GetUserDto, RegisterUserRequest>()
                  .ForMember(d => d.User, o => o.MapFrom(s => "ADMIN"));
            });
            mapper = new Mapper(config);
        }

        public Result<string> CreateUser(GetUserDto dto)
        {
            var createUserConector = serviceJwt.RegisterUser(mapper.Map<RegisterUserRequest>(dto));
            if (!createUserConector.Header.IsOk)
            {
                return Result<string>.SetError("Error en el conector");
            }
            if (!createUserConector.Body.IsOk)
            {
                return Result<string>.SetError("Error al momento de crear el usuario contactarse con el administrador");
            }
            if (dto.NotifyUser)
            {
                var parameter = GetParameter("NOTSB", "TXUSCR");
                if(parameter == null)
                {
                    return Result<string>.SetOk($"Usuario {dto.AccessNumber} creado con éxito, notificacion no enviada");
                }
                var text = parameter.Description.Replace("<UserName>", dto.AccessNumber).Replace("<User>", $"{dto.Name} {dto.FirstLastName} {dto.SecondLastName}");
                messageNotification = SendNotification(createUserConector.Body.Body.UserId, text, dto.PhoneNumber, dto.AccessNumber);
            }
            return Result<string>.SetOk($"Usuario {dto.AccessNumber} creado con éxito, {messageNotification}");
        }

        public Result<List<GetUserResult>> GetListUsers(GetUserDto dto)
        {
            var listUsers = Context.Users.Where(x => x.IsActive).ToList();
            if (dto.State != string.Empty && dto.State != null)
            {
                listUsers = listUsers.Where(x => x.State == dto.State).ToList();
            }
            if (dto.AccessNumber != string.Empty && dto.AccessNumber != null)
            {
                listUsers = listUsers.Where(x => x.UserName.Contains(dto.AccessNumber)).ToList();
            }
            totalItems = listUsers.Count;
            var listpag = listUsers.OrderBy(x => x.Id).Skip(dto.PageSize * (dto.CurPage - 1)).Take(dto.PageSize).ToList();
            return listUsers.Any() ? Result<List<GetUserResult>>.SetOk(mapper.Map<List<GetUserResult>>(listpag)) :
                Result<List<GetUserResult>>.SetError("No existen Usuarios Registrados");
        }

        public Result<GetUserResult> GetUserId(GetUserDto dto)
        {
            var user = GetUser(dto.userId);
            return user == null ? Result<GetUserResult>.SetOk(mapper.Map<GetUserResult>(user))
                : Result<GetUserResult>.SetError($"El usuario {dto.AccessNumber} no existe");
        }

        public Result<string> UpdateUser(GetUserDto dto)
        {
            Context.DisableFilter(Context, "IsDeleted");
            var user = GetUser(dto.userId);
            if (user == null)
            {
                return Result<string>.SetError("El usuario no existe");
            }
            user.UserName = dto.AccessNumber;
            user.UserDetail.Name = dto.Name;
            user.UserDetail.FirstLastName = dto.FirstLastName;
            user.UserDetail.SecondLastName = dto.SecondLastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.DateModification = DateTime.Now;
            foreach (var rol in dto.Roles)
            {
                var userRol = user.UserRoles.Where(x => x.RoleId == rol.RoleId).FirstOrDefault();
                if (userRol != null)
                {
                    userRol.IsDeleted = rol.IsDeleted;
                    userRol.DateModification = DateTime.Now;
                }
                else
                {
                    user.UserRoles.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = rol.RoleId,
                        IsDeleted = true,
                        DateCreation = DateTime.Now
                    });
                }
            }
            Context.Save(user);
            return Result<string>.SetOk($"El usuario {user.UserName} fue actualizado con éxito");

        }

        public Result<string> ChangeStateUser(GetUserDto dto)
        {
            var user = GetUser(dto.userId);
            if (user == null)
            {
                Result<string>.SetError($"El usuario {dto.AccessNumber} no existe");
            }
            user.State = dto.State;
            user.DateModification = DateTime.Now;
            user.DateLastPasswordChange = DateTime.Now;
            Context.Save(user);
            if (dto.State == "G")
            {
                var parameter = GetParameter("NOTSB", "TXUSDS");
                if (parameter == null)
                {
                    return Result<string>.SetOk($"El usuario {dto.AccessNumber} fue desbloqueado con éxito, notificación no enviada");
                }
                var text = parameter.Description.Replace("<UserName>", user.UserName.Trim());
                messageNotification = SendNotification(user.Id, text, user.PhoneNumber.Trim(), user.UserName.Trim());
            }
            return Result<string>.SetOk($"El usuario {dto.AccessNumber} fue desbloqueado con éxito, {messageNotification}");
        }

        public Result<string> DeleteUser(GetUserDto dto)
        {
            var user = GetUser(dto.userId);
            if (user == null)
            {
                Result<string>.SetError($"El usuario {dto.AccessNumber} no existe");
            }
            user.IsActive = false;
            user.DateModification = DateTime.Now;
            Context.Save(user);
            return Result<string>.SetOk($"El usuario {dto.AccessNumber} fue deshabilitado con éxito");
        }

        public Result<string> UnlockAllUsers()
        {
            var user = Context.Users.Where(x => x.State != "G").ToList();
            user.ForEach(x => x.State = "G");
            Context.SaveChanges();
            return Result<string>.SetOk($"Todos los usuarios fueron habilitados con exito");
        }

        private string SendNotification(int userId, string text, string phoneNumber, string uid)
        {
            string result = string.Empty;
            uid = $"{DateTime.Now.ToString("yyyymmddhhmmss")}{uid}";
            var notifyUser = serviceBot.SendMessageWsp(new SendMessageWspRequest
            {
                Uid = uid,
                To = $"591{phoneNumber}",
                Text = text
            });
            result = notifyUser.Header.IsOk ? "Notificación enviada correctamente"
                : string.IsNullOrEmpty(notifyUser.Header.FirstError) ? notifyUser.Header.Exception.Message : notifyUser.Header.FirstError;
            Context.Save(new SendNotification
            {
                SendPhone = phoneNumber,
                UserId = userId,
                Status = notifyUser.Header.IsOk,
                UID = uid,
                MessageStatus = result,
                DateCreation = DateTime.Now
            });
            return result;
        }

        private User GetUser(int userId) => Context.Users.Where(x => x.Id == userId).FirstOrDefault();
    }
}
