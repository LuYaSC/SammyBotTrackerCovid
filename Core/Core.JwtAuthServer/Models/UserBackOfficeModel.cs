//using TC.Connectors.Segurinet;

namespace TC.Core.JwtAuthServer.Models
{
    public class UserBackOfficeModel
    {
        //public AuthenticationStatus Status { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherLastName { get; set; }
        public string[] Roles { get; set; }
        public string[] Policies { get; set; }
    }

    public class BackOfficeRoles
    {
        public const string superAdmin = "superAdmin";
        public const string adminBank = "adminBank";
        public const string backOffice = "backOffice";
        public const string backOfficeBG = "backOfficeBG";
        public const string consultant = "consultant";
        public const string preparatorFud = "preparatorFud";
        public const string perfilFTP = "perfilFTP";
    }

    public class PerfilesOfficeBanking
    {
        /// Usuario Adm de Empresa
        public const string CW_ADMIN = "CW_ADMIN";
        /// Usuario autorizador de paquete
        public const string CW_AUTORIZADOR = "CW_AUTORIZADOR";
        /// Usuario de Cartera
        public const string CW_CARTERA = "CW_CARTERA";
        /// Usuario preparador de paquetes
        public const string CW_PREPARADOR = "CW_PREPARADOR";
        /// Super usuario del sistema
        public const string CW_SUPER_ADM = "CW_SUPER_ADM";
        /// Usuario de Tele Procesos
        public const string CW_TELEPROC = "CW_TELEPROC";
        /// Usuario de Transacciones Financieras
        public const string CW_TRANSACFIN = "CW_TRANSACFIN";
        /// Usuario de Acceso para prevencion y cumplimiento
        public const string CW_PREVENCION = "CW_PREVENCION";
        /// Usuario de Acceso para procedimiento de credito
        public const string CW_PROCCREDITO = "CW_PROCCREDITOS";
        /// Usuario de Acceso para funcionario de negocios
        public const string CW_FUNNEGOCIO = "CW_FUNNEGOCIOS";
        /// Usuario de Acceso para controladores
        public const string CW_SUPERVISOR = "CW_SUPERVISOR";
        /// Usuario de Acceso para controladores
        public const string CW_OPERADOR = "CW_OPERADOR";
        /// Usuario de Acceso para fiscal
        public const string CW_FISCAL = "CW_FISCAL";
        /// Usuario de Acceso para consultores
        public const string CW_CONSULTOR = "CW_CONSULTOR";
        /// Usuario de Acceso para preparador
        public const string CW_FX_PREPARADOR = "CW_FX_PREPARADOR";
        /// Usuario de Acceso para autorizador
        public const string CW_FX_AUTORIZADOR = "CW_FX_AUTORIZADOR";
        /// Usuario de Acceso para preaparar fud
        public const string CW_ACH_FUD = "CW_ACH_FUD";
        /// Usuario de CONFIGURA LOS datos para FTP
        public const string CW_FTP = "CW_FTP";
    }
}
