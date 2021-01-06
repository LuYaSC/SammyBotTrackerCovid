namespace TC.Core.JwtAuthServer.Messages
{
    public class AuthMessages : IAuthMessages
    {
        public string ErrorLogin => "El Número de Acceso o la Contraseña son incorrectos.";

        public string AccessLocked => "Por razones de seguridad su usuario ha sido bloqueado. Para desbloquear el usuario contactese con 68216880";

        public string ErrorPasswordHistory => "No puede asignar una clave de internet que haya sido utilizada anteriormente. Asigne una nueva.";

        public string InternalError => "Error Interno del Sistema.";

        public string LoginGenerate => "Su clave de internet fué generada correctamente.";

        public string LoginChange => "Su clave de internet fué cambiada correctamente.";

        public string LoginExpirate => "Su Clave de Internet ha expirado, por favor debe generar una nueva.";

        public string LoginReset => "Debe crear una clave de internet para poder ingresar a la aplicación.";

        public string GenerateError => "El número de Tarjeta ya tiene registrada una clave de internet.";

        public string Captcha => "El texto introducido no coincide con la imagen.";

        public string ErrorLoginBackOffice => "El Número de Acceso o la Contraseña del Back Office son incorrectos.";

        public string Recaptcha => "No se pudo validar el captcha, por favor intente nuevamente";

    }
}