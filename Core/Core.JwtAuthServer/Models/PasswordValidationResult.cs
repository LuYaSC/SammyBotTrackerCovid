namespace TC.Core.JwtAuthServer.Models
{
    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}