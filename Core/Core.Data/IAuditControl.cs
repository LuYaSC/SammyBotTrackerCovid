namespace TC.Core.Data
{
    public interface IAuditControl
    {
        int TimeStampCreacion { get; set; }

        int TimeStampModificacion { get; set; }

        string UsuarioCreacion { get; set; }

        string UsuarioModificacion { get; set; }
    }
}
