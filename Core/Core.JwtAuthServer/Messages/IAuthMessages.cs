namespace TC.Core.JwtAuthServer.Messages
{
    public interface IAuthMessages
    {
        string ErrorLogin { get; }
        string AccessLocked { get; }
        string ErrorPasswordHistory { get; }
        string InternalError { get; }
        string LoginGenerate { get; }
        string LoginChange { get; }
        string LoginExpirate { get; }
        string LoginReset { get; }
        string GenerateError { get; }
        string Captcha { get; }

    }
}
