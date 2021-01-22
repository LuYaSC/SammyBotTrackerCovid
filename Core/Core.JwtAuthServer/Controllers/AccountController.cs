namespace TC.Core.JwtAuthServer.Controllers
{
    using TC.Core.JwtAuthServer.Messages;
    using TC.Core.JwtAuthServer.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using TC.Core.JwtAuthServer.Entities;
    using TC.Core.JwtAuthServer.Extensions;

    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private const string LocalLoginProvider = "Local";

        private ApplicationUserManager userManager;
        //private CaptchaManager captchaManager;
        //private ISmartLinkManager smartLinkManager;
        private IAuthMessages messages;

        public AccountController()
        {
            //this.smartLinkManager = new SmartLinkManager();
            this.messages = new AuthMessages();
            //this.captchaManager = new CaptchaManager();
        }

        public AccountController(
            ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            this.UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo(int id)
        {
            User user = this.UserManager.FindById(id);
            return new UserInfoViewModel
            {
                Name = user.UserName,
            };
        }

        [Route("Claims")]
        public ClaimsModel Claims()
        {
            var claims = new ClaimsModel();
            claims.CompanyId = User.Identity.GetCompanyId();
            claims.CompanyName = User.Identity.GetCompanyName();
            claims.CompanyState = User.Identity.GetCompanyState();
            claims.UserId = User.Identity.GetUserId();
            //claims.ControllerScheme = User.Identity.GetControllerScheme();
            claims.UserName = User.Identity.GetFullUserName();
            claims.UserType = User.Identity.GetUserType();
            //claims.IsSignatureSismac = User.Identity.GetIsSignature();
            //identity = User.Identity.GetUserId();
            return claims;
        }

        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        //[AllowAnonymous]
        //[Route("CreatePassword")]
        //public async Task<IHttpActionResult> VerifyAccessNumber(PasswordModel model)
        //{
        //    //Validacion

        //    return Ok();
        //}

        [AllowAnonymous]
        //[Route("ValidatePin")]
        //[FilterCaptcha]
        //[RecaptchaValidate]
        //[CwCardValidate]
        //[FilterSmartlink]
        public async Task<Result<bool>> ValidatePinAsync(ValidatePinRequestModel dto)
        {
            var user = await this.UserManager.FindByNameAsync(dto.Card);
            if (user == null)
            {
                return Result<bool>.SetError("invalid_username");
            }
            return Result<bool>.SetOk(true);
        }

        //[AllowAnonymous]
        [Route("ValidateNewPassword")]
        //[FilterCaptcha]
        //[RecaptchaValidate]
        public async Task<IHttpActionResult> ValidateNewPassword(NewPasswordRequestModel dto)
        {
            if (dto.NewPassword == dto.ConfirmPassword)
            {
                return this.Ok();
            }
            return this.BadRequest();
        }

        [AllowAnonymous]
        [Route("CreatePassword")]
        //[FilterCaptcha]
        //[RecaptchaValidate]
        //[FilterSmartlink]
        public async Task<IHttpActionResult> CreatePassword(NewPasswordRequestModel dto)
        {
            //User user = await this.UserManager.FindByIdAsync(newPassword.AccessNumber);
            var user = await this.UserManager.FindByNameAsync(dto.AccessNumber);
            HttpResponseMessage httpResponse;

            //if (!captchaManager.VerifyCaptcha(new VerifyCaptcha() { Value = newPassword.CaptchaValue, ValueToVerify = newPassword.CaptchaValueToVerify }))
            //{
            //    httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.Captcha);
            //    return this.ResponseMessage(httpResponse);
            //}

            if (user == null)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.ErrorLogin);
                return this.ResponseMessage(httpResponse);
            }

            if (user.PasswordHash != null)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.GenerateError);
                return this.ResponseMessage(httpResponse);
            }

            if (!(user.State == UserStore.USER_STATE_RESET || user.State == UserStore.USER_STATE_NEW))
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.GenerateError);
                return this.ResponseMessage(httpResponse);
            }

            user.PasswordHash = this.UserManager.PasswordHasher.HashPassword(dto.NewPassword);
            user.DateLastPasswordChange = DateTime.Today;
            user.State = UserStore.USER_STATE_GENERATE;
            user.AccessFailedCount = 0;
            IdentityResult result = null;

            result = await this.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, this.messages.InternalError);
                return this.ResponseMessage(httpResponse);
            }

            SessionStore sessionStore = new SessionStore();
            sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_GENERATE, dto.IpClient);

            httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.OK, this.messages.LoginGenerate);
            return this.ResponseMessage(httpResponse);
        }

        [AllowAnonymous]
        [Route("CreatePasswordR")]
        //[FilterCaptcha]
        //[RecaptchaValidate]
        //[FilterSmartlink]
        public Result<RegisterResult> CreatePasswordR(NewPasswordRequestModel dto)
        {
            //User user = await this.UserManager.FindByIdAsync(newPassword.AccessNumber);
            var user = this.UserManager.FindByName(dto.AccessNumber);

            //if (!captchaManager.VerifyCaptcha(new VerifyCaptcha() { Value = newPassword.CaptchaValue, ValueToVerify = newPassword.CaptchaValueToVerify }))
            //{
            //    httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.Captcha);
            //    return this.ResponseMessage(httpResponse);
            //}

            if (user == null)
            {
                return Result<RegisterResult>.SetError("El usuario no existe");
            }

            if (user.PasswordHash != null)
            {
                return Result<RegisterResult>.SetError("La contraseña no puede estar vacia");
            }

            if (!(user.State == UserStore.USER_STATE_RESET || user.State == UserStore.USER_STATE_NEW))
            {
                return Result<RegisterResult>.SetError("El usuario debe estar en estado Reseteado o Nuevo");
            }

            user.PasswordHash = this.UserManager.PasswordHasher.HashPassword(dto.NewPassword);
            user.DateLastPasswordChange = DateTime.Today;
            user.State = UserStore.USER_STATE_GENERATE;
            user.AccessFailedCount = 0;
            IdentityResult result = null;

            result = this.UserManager.Update(user);

            if (!result.Succeeded)
            {
                return Result<RegisterResult>.SetError("No se pudo crear la contraseña contactese con el administrador");
            }

            SessionStore sessionStore = new SessionStore();
            sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_GENERATE, dto.IpClient);

            return Result<RegisterResult>.SetOk(new RegisterResult { Message = "se creo correctamente" });
        }

        [AllowAnonymous]
        [Route("ChangePassword")]
        //[FilterCaptcha]
        //[RecaptchaValidate]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordRequestModel dto)
        {
            User user = await this.UserManager.FindAsync(dto.AccessNumber, dto.OldPassword);
            HttpResponseMessage httpResponse;

            if (user == null)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.ErrorLogin);
                return this.ResponseMessage(httpResponse);
            }

            if (user.State == UserStore.USER_STATE_LOCKED)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.AccessLocked);
                return this.ResponseMessage(httpResponse);
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.ErrorLogin);
                return this.ResponseMessage(httpResponse);
            }

            SessionStore sessionStore = new SessionStore();
            ParameterStore parameterStore = new ParameterStore();

            bool isValidHistory = IsValidPasswordHistoy(sessionStore.GetHistoyPasswordChange(user, parameterStore.GetNumberPasswordVerifyHistory()), dto.NewPassword);

            if (!isValidHistory)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, this.messages.ErrorPasswordHistory);
                return this.ResponseMessage(httpResponse);
            }

            //user.PasswordHash = this.UserManager.PasswordHasher.HashPassword(model.NewPassword);

            IdentityResult result = await this.UserManager.ChangePasswordAsync(
                user.Id,
                dto.OldPassword,
                dto.NewPassword);

            if (!result.Succeeded)
            {
                httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, this.messages.ErrorLogin);
                return this.ResponseMessage(httpResponse);
                //return this.GetErrorResult(result);
                //return this.BadRequest("Ocurrio un error, por favor, verifique los datos y vuelva a intentar");
            }
            User userTemp = await this.UserManager.FindAsync(dto.AccessNumber, dto.NewPassword);

            sessionStore.SaveSession(userTemp, SessionStore.TYPE_SESSION_CHANGE, dto.IpClient);

            user.DateModification = DateTime.Now;
            user.DateLastPasswordChange = DateTime.Now;
            user.State = UserStore.USER_STATE_CHANGE;

            await this.UserManager.UpdateAsync(user);

            // Delete all refreshtokens for this user on all client_ids
            AudienceStore store = new AudienceStore();
            await store.RemoveAllRefreshTokenForUser(dto.AccessNumber);


            httpResponse = Request.CreateResponse(System.Net.HttpStatusCode.OK, this.messages.LoginChange);
            return this.ResponseMessage(httpResponse);
        }

        [HttpGet]
        [Route("ListByRole")]
        public List<User> ListByRole(string roleName)
        {
            var store = new UserStore();
            var companyId = User.Identity.GetCompanyId();
            var userId = User.Identity.GetUserId();
            //var empresaId = int.Parse(identity.FindFirst("empresaId").Value);
            //var clientId = identity.FindFirst("aud").Value;
            return store.ListByRole(int.Parse(userId), roleName);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [Filters.ValidationModel]
        public Result<AccessCardResult> Register(RegisterBindingModel model)
        {
            if (!model.Roles.Any())
            {
                return Result<AccessCardResult>.SetError("El usuario a crearse no tiene roles asignados");
            }
            var roles = new List<UserRole>();
            foreach (var role in model.Roles)
            {
                roles.Add(new UserRole
                {
                    RoleId = role.RoleId,
                    DateCreation = DateTime.Now,
                });
             }
            var user = new User()
            {
                UserName = model.AccessNumber,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                State = UserStore.USER_STATE_GENERATE,
                AvailableDays = 120,
                IsActive = true,
                DateCreation = DateTime.Now,
                DateLastPasswordChange = DateTime.Now,
                DateModification = DateTime.Now,
                PasswordHash = this.UserManager.PasswordHasher.HashPassword(model.NewPassword),
                AccessFailedCount = 0,
                UserDetail = new UserDetail
                {
                    Name = model.Name,
                    FirstLastName = model.FirstLastName,
                    SecondLastName = model.SecondLastName,
                    DateCreation = DateTime.Now,
                    UserCreation = model.User,
                },
                UserRoles = roles
            };
            IdentityResult result = UserManager.Create(user);
            if (!result.Succeeded)
            {
                return Result<AccessCardResult>.SetError(result.Errors.FirstOrDefault());
            }
            SessionStore sessionStore = new SessionStore();
            sessionStore.SaveSession(user, SessionStore.TYPE_SESSION_GENERATE, model.IpClient);
            return Result<AccessCardResult>.SetOk(new AccessCardResult { UserId = user.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private bool IsValidPasswordHistoy(IEnumerable<Session> lastChangesPassword, string newPassword)
        {
            foreach (var item in lastChangesPassword)
            {
                var resultTemp = this.UserManager.PasswordHasher.VerifyHashedPassword(item.Password, newPassword);

                if (resultTemp == PasswordVerificationResult.Success)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
