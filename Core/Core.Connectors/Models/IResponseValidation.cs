namespace TC.Core.Connectors.Models
{
    public interface IResponseValidation
    {
        bool IsValid { get; set; }

        string ErrorMessage { get; set; }
    }
}
