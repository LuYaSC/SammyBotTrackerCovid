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
            var user = new User()
            {
                UserName = model.AccessNumber,
                //CompanyId = model.CompanyId,
                State = UserStore.USER_STATE_NEW,
                //UserCreation = model.UserId,
                DateCreation = DateTime.Now
            };
            IdentityResult result = UserManager.Create(user);
            if (!result.Succeeded)
            {
                return Result<AccessCardResult>.SetError(result.Errors.FirstOrDefault());
            }
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

        //[AllowAnonymous]
        //[Route("GetCaptcha")]
        //[HttpPost]
        //public CaptchaModel GetCaptcha()
        //{
        //    return captchaManager.GetCaptcha();
        //}

        //[Route("ValidatePassword")]
        //public Result<PasswordValidationResult> ValidatePassword(ValidatePasswordRequestModel dto)
        //{
        //    User user = UserManager.FindByName(User.Identity.GetUserName());
        //    if (user.State == UserStore.USER_STATE_LOCKED)
        //    {
        //        return Result<PasswordValidationResult>.SetOk(new PasswordValidationResult { IsValid = false, ErrorMessage = messages.AccessLocked });
        //    }
        //    User loggedUser = UserManager.Find(user.UserName, dto.Password);
        //    if (loggedUser == null)
        //    {
        //        if (user.AccessFailedCount == (Convert.ToInt32(ConfigurationManager.AppSettings["maxFailedAccess"]) - 1))
        //        {
        //            user.State = UserStore.USER_STATE_LOCKED;
        //            UserManager.Update(user);
        //            UserManager.ResetAccessFailedCount(user.Id);
        //            return Result<PasswordValidationResult>.SetOk(new PasswordValidationResult { IsValid = false, ErrorMessage = messages.AccessLocked });
        //        }
        //        UserManager.AccessFailed(user.Id);
        //        return Result<PasswordValidationResult>.SetOk(new PasswordValidationResult { IsValid = false, ErrorMessage = messages.ErrorLogin });
        //    }
        //    return Result<PasswordValidationResult>.SetOk(new PasswordValidationResult { IsValid = true });
        //}

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
