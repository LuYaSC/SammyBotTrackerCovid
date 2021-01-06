namespace TC.Core.Business
{
    using System.Collections;
    using System.Collections.Generic;

    public class BaseConstants
    {
        public const int PAYMENTMETHOD_EFFECTIVE = 1;
        public const int PAYMENTMETHOD_CHECK = 2;
        public const int PAYMENTMETHOD_CARD = 3;
        public const int PAYMENTMETHOD_OTHER = 4;

        public const int BILLINGQUERYTYPERESPONSE_BATCH = 0;
        public const int BILLINGQUERYTYPERESPONSE_CUFBILLROLL = 1;
        public const int BILLINGQUERYTYPERESPONSE_CUFBILLLETTER = 2;
        public const int BILLINGQUERYTYPERESPONSE_CUFOBJECTBILL = 3;

        public const string CURRENCY_BOL = "BOL";
        public const string CURRENCY_BOL_EXTENDED = "BOLIVIANOS";
        public const string CURRENCY_USD = "USD";
        public const string CURRENCY_USD_EXTENDED = "DÓLARES";
        public const string PATH_REPORT = @"\\172.31.12.64\ReportsCW\";
        public const string REPORT_GROUP = "REPORT";
        public const string REPORT_CODE = "PATH";
        public const string XML_EXTENSION = "<?xml";
        public const string BALLOT_OF_WARRANTY = "BDG";
        public const string BALLOT_OF_WARRANTY_CONTRACT = "BG1";
        public const string FIRST_REQUIREMENT_WARRANTY = "GPR";
        public const string FIRST_REQUIREMENT_WARRANTY_CONTRACT = "PR1";

        public const string BALLOT_OF_WARRANTY_EXTENDED = "BOLETA DE GARANTÍA";
        public const string REQUIREMENT_WARRANTY_EXTENDED = "GARANTÍA A PRIMER REQUERIMIENTO";

        public const string NATURAL_PERSON = "PN";
        public const string NATURAL_PERSON_EXTENDED = "PERSONA NATURAL";
        public const string LEGAL_PERSON = "PJ";
        public const string LEGAL_PERSON_EXTENDED = "PERSONA JURÍDICA";

        public const string DOCUMENT_TYPE = "TIPDOC";
        public const string CURRENCY_TYPE = "TIPMON";
        public const string DOCUMENT_EXTENSION = "LUGEXT";
        public const string PARAM_UIF_AMOUNT_VALIDATION = "MINUIF";
        public const string CODE_BUSINESS_FUNCIONARY = "FFNN";
        public const string SUCURSAL_CODE = "CODSUC";
        public const string SUCURSALOF_CODE = "CODOF";

        public const int CRE_SERVICE = 1;
        public const int SAGUAPAC_SERVICE = 11;
        public const int DELAPAZ_SERVICE = 2;
        public const int FIXED_TELEPHONY_SERVICE = 3;
        public const int MOBILE_TELEPHONY = 4;
        public const int RUAT_VEHICLE_SERVICE = 5;
        public const int RUAT_PROPERTY_SERVICE = 6;
        public const int ELFEC_PROPERTY_SERVICE = 7;

        //Pagos Masivos y Favoritos
        public const string PAGO_HABERES = "HAB";
        public const string PAGO_PROVEEDORES = "PROV";
        public const string PAGO_EFECTIVO = "EFE";
        public const string PAGO_ACH = "ACH";
        public const string PAGO_ACHODD = "ACHOD";
        public const string CONFIG_OF_FAVORITE_PAYMENT = "CONFIG_OF_FAVORITE_PAYMENT";
        public const string REQUEST_CERTIFICATION = "REQUEST_CERTIFICATION";
        public const string REQUEST_CLAIMS = "REQUEST_CLAIMS";
        public const string REQUEST_MODIFICATIONS = "REQUEST_MODIFICATIONS";
        public const string EXTENSION_DOC_WORD = ".doc";

        public const string IDENTITY_CARD = "Q";
        public const string FISCAL_ID = "W";
        public const string FISCAL_ID_EXTENDED = "I.D. FISCAL";
        public const string NIT = "T";
        public const string NIT_EXTENDED = "NIT";
        public const string PASSPORT = "P";
        public const string RUN = "U";
        public const string RUN_EXTENDED = "RUN";
        public const string RUC = "R";
        public const string OTHER = "O";
        public const string DEFAULT_EXTENSION = "000";

        public const int ROLE_AUTHORIZER = 1;
        public const int ROLE_CONSULTANT = 2;
        public const int ROLE_CONTROLLER = 3;
        public const int ROLE_INITIATOR = 4;

        public const int OPERATION_STATUS_SOLICITED = 1;
        public const int OPERATION_STATUS_CONTROLLED = 2;
        public const int OPERATION_STATUS_AUTHORIZED = 3;
        public const int OPERATION_STATUS_IN_PROCESS = 4;
        public const int OPERATION_STATUS_AUTOMATIC_PROCESSED = 5;
        public const int OPERATION_STATUS_MANUAL_PROCESSED = 6;
        public const int OPERATION_STATUS_AUTOMATIC_REJECTED = 7;
        public const int OPERATION_STATUS_ERROR = 8;
        public const int OPERATION_STATUS_REJECTED = 9;
        public const int OPERATION_STATUS_BACK_OFFICE_IN_PROCESS = 10;
        public const int OPERATION_STATUS_SAVED_PENDING_SOLICITUDE = 11;
        public const int OPERATION_STATUS_BACK_OFFICE_PENDING = 12;
        public const int OPERATION_STATUS_NOT_PRE_PREPARED = 13;

        public const int OPERATION_STATUS_ASSIGNED = 13;
        public const int OPERATION_STATUS_OBSERVED = 14;
        public const int OPERATION_STATUS_REJECT_BO = 15;
        public const int OPERATION_STATUS_PRE_SAVED_EXTERIOR = 17;

        public const int ACCOUNT_SAVINGS_LENGTH = 14;
        public const int CURRENT_ACCOUNT_LENGTH = 13;

        public const string TYPE_SAVINGS = "AHO";
        public const string TYPE_SAVINGS_EXTENDED = "CUENTA DE AHORRO";
        public const string TYPE_CURRENT = "CTE";
        public const string TYPE_CURRENT_EXTENDED = "CUENTA CORRIENTE";

        public const string TYPE_BUSINESS = "COL";
        public const string TYPE_BUSINESS_EXTENDED = "CUENTA EMPRESARIAL";

        public const string TICKET_STATUS_SOLICITED = "P";
        public const string TICKET_STATUS_PROCESSED = "L";

        //TODO Cambiar el tipo de dato de la columna State en cw.UserNotifications
        public const string USER_NOTIFICATION_STATUS_PRE_REGISTERED = "G";
        public const string USER_NOTIFICATION_STATUS_REGISTERED = "R";
        public const string USER_NOTIFICATION_STATUS_TO_SEND = "N";
        public const string USER_NOTIFICATION_STATUS_SENT = "E";

        public const int OPERATION_TRANS_AL_EXTERIOR_CON_CAMBIO_D = 7;
        public const int OPERATION_PAGO_HABERES = 8;
        public const int OPERATION_PAGO_DE_SERVICIOS = 9;
        public const int OPERATION_PAGO_DE_SERVICIOS_TELEFONIA = 10;
        public const int OPERATION_CONSULTAR_CUENTAS = 12;
        public const int OPERATION_TRANSFERENCIAS_CUENTAS_PROPIAS = 13;
        public const int OPERATION_TRANSFERENCIAS_CUENTAS_TERCEROS = 14;
        public const int OPERATION_TRANSFERENCIAS_NACIONALES = 15;
        public const int OPERATION_PAGO_PROVEEDORES = 16;
        public const int OPERATION_FORMULARIO_SOLICITUD = 17;
        public const int OPERATION_BOLETA_GARANTIA = 18;
        public const int OPERATION_IDENTIFICACION_DE_ABONOS = 19;
        public const int OPERATION_PAGO_MULTIPLE = 20;
        public const int OPERATION_PAGO_CREDITO_EMPRESARIAL = 21;
        public const int OPERATION_PAGO_FAVORITO = 23;
        public const int OPERATION_PAGO_PROVEEDORES_ACH = 24;
        public const int OPERATION_PAGO_PROVEEDORES_EFE = 25;
        public const int OPERATION_PAGO_PROVEEDORES_CHEQUE_GERENCIA = 26;
        public const int OPERATION_PAGO_PROVEEDORES_OTROS_BANCOS_CHEQUE = 27;
        public const int OPERATION_PAGO_IMPUESTO_GERENCIA = 28;
        public const int OPERATION_ORDENES_DE_DEBITO_A_OTROS_BANCOS = 29;
        public const int OPERATION_FORMULARIO_MODIFICACION = 30;
        public const int OPERATION_CONFIGURACION_DE_PAGOS_FAVORITOS = 31;
        public const int OPERATION_SERVICE_ELFEC = 32;
        public const int OPERATION_SERVICES_PASE = 33;
        public const int OPERATION_SERVICE_EPSAS = 34;


        public const string ACCOUNT_USE_DEBIT = "D";
        public const string ACCOUNT_USE_CREDIT = "C";

        public const string ACCOUNT_TYPE_PASIVA_HOST = "P";
        public const string ACCOUNT_TYPE_ACTIVA_REPEXT = "A";
        public const string ACCOUNT_TYPE_SERVICE = "S";
        public const string ACCOUNT_TYPE_CREDIFONDO = "C";

        public const string OLDEST_DATE = "1900-01-01";

        public const int MONDAY = 1;
        public const int TUESDAY = 2;
        public const int WEDNESDAY = 3;
        public const int THURSDAY = 4;
        public const int FRIDAY = 5;
        public const int SATURDAY = 6;
        public const int SUNDAY = 7;

        public const string SERVICE_CHANNEL = "CW";

        public const string PREPARED_BATCH_VALIDATION_ERROR_MESSAGE = "Ya se preparó este pago.Verifique en pendientes o seguimiento.";

        public const string PROCESS_TYPE_MANUAL = "M";
        public const string PROCESS_TYPE_AUTOMATIC = "A";

        public const string SIG_CURRENCY_BOL = "BOB";
        public const string SENDING_TYPE_LOCAL = "L";
        public const string SENDING_TYPE_EXTERIOR = "E";
        public const string TYPE_IDC_NIT = "T";
        public const string TYPE_IDC_RUC = "R";

        public const string PURCHASE_EXCHANGE_RATE = "C";
        public const string SALE_EXCHANGE_RATE = "V";

        public const int ACCOUNT_NUMBER_LENGTH = 26;
        public const string ACCOUNT_NUMBER_SEPARATOR = "-";
        public const string ACCOUNT_NUMBER_INIT = "1030";
        public const string CURRENT_ACCOUNT_NUMBER_IDENTIFIER = "00000000000";
        public const string ACCOUNT_SAVINGS_NUMBER_IDENTIFIER = "0001000000";

        public const string GLOSS_INIT = "CW-";

        public const int FIFTY_THOUSAND = 50000;
        public const int HUNDRED_THOUSAND = 100000;
        public const System.Int64 BIG_VALUE = 9999999999999999;

        public const string TOKEN_ACCESS_DENIED_MESSAGE = "La clave dinámica del token es inválida";
        public const int OPERATION_TYPE_TRANSFER_SEMIAUTOMATIC = 0;
        public const int OPERATION_TYPE_TRANSFER_AUTOMATIC = 1;
        public const int OPERATION_TYPE_TRANSFER_MANUAL = 2;

        public const string MESSAGE_PROCESSED = "PROCESADO";
        public const string MESSAGE_ERROR_PROCESSED = "ERROR NO PROCESADO";

        public const string PATRONCUENTAVISIBLE = @" ^ (\d{3}-\d{7,8}-\d-\d\d)|(\d{13,14})$";
        public const string EntidadBanco = "10";
        public const string PrefijoMonedas = "30";
        public const string RellenoCuentaAhorro = "0001000000";
        public const string RellenoCuentaCorriente = "00000000000";

        public const string NumeroValorCuentaMonedaUSD = "2";
        public const string NumeroValorcuentaMonedaBOL = "3";
        public const string ConstanteCuenta = "000";
        public const string ValorProductoAHO = "1";
        public const string ValorProductoCTE = "0";
        public const string ConstanteCuentaCTE = "0000000";
        public const string ConstanteCuentaAHO = "000000";

        public const string CodigoCuentaCorriente = "CTE";
        public const string CodigoCuentaAhorro = "AHO";

        public const int COMMISSION_DEPOSIT_DETECTION = 1;
        public const int COMMISSION_CREDINET_WEB_SERVICE = 2;
        public const int COMMISSION_TOKEN = 3;
        public const int COMMISSION_CARD = 4;

        public const int COMMISSION_STATUS_PENDING = 1;
        public const int COMMISSION_STATUS_PARTIAL = 2;
        public const int COMMISSION_STATUS_COMPLETE = 3;
        public const int COMMISSION_STATUS_TRIED = 4;
        public const int COMMISSION_STATUS_IN_PROCESS = 0;

        public const int CHARGE_COMMISSION_STATUS_NORMAL = 1;
        public const int CHARGE_COMMISSION_STATUS_ACCRUED = 2;
        public const int CHARGE_COMMISSION_STATUS_RECOVERED = 3;
        public const int CHARGE_COMMISSION_STATUS_TRIED = 4;
        public const int CHARGE_COMMISSION_STATUS_FREE = 5;
        
        public const string COMMISSION_BILL_PENDING = "S";
        public const string COMMISSION_BILL_BILLED = "F";
        public const string COMMISSION_BILL_ERROR = "X";
        public const string COMMISSION_BILL_WITHOUT = "W";

        public const string CODE_HOUR = "HORA";
        public const string CODE_RECALCULATING = "RECALC";
        public const string CODE_COMPLETE_CREDITS = "IDCRED";

        public const string CODE_COMPLETE_HISTORICAL = "COMHIS";

        public const string CODE_DAY = "DIAS";
        public const string CODE_COMPLETE_PASE = "COMPAS";
        public const string CODE_ACCOUNT_REC = "CTAREC";
        public const string CODE_ACCOUNT_SER = "CTASER";

        public const string MULTIPLE_DEBIT_ACH = "M";
        public const string ONE_DEBIT_ACH = "T";
        public const string CURRENCY_BOL_ACH = "BOB";

        public const int GENERAL_REPORT = 1;
        public const int DETAILED_REPORT = 2;
        public const int FRACTIONATED_REPORT = 3;
        public const int FILE_TXT = 4;

        public static IDictionary TRANSFER_TYPES = new Dictionary<int, string>
        {
            { (int)TransferTypes.own, "TRANSFERENCIA ENTRE CUENTAS PROPIAS" },
            { (int)TransferTypes.thirdParty, "TRANSFERENCIA A CUENTAS DE TERCEROS" },
            { (int)TransferTypes.interbank, "TRANSFERENCIA A OTRO BANCO" }
        };

        public const int TRANSFERENCIAS_CUENTAS_PROPIAS = 0;
        public const int TRANSFERENCIAS_CUENTAS_TERCEROS = 1;
        public const int TRANSFERENCIA_OTRO_BANCO = 2;

        public const string BOLETA_GARANTIA = "BG";

        public const string PIGNORACION_DE_FONDOS = "PDF";
        public const string DEPOSITO_PLAZO_FIJO = "DPF";
        public const string LINEA_DE_CREDITO = "LRC";

        public const string FFNN = "FUNCIONARIO DE NEGOCIOS";
        public const string FISCAL = "FISCAL";
        public const string SUPERVISOR = "SUPERVISOR";
        public const string OPERADOR = "OPERADOR";
        public const string SUPERVISOR_FISCAL = "SUPERVISOR/FISCAL";

        public const int AUTHORIZATION_TRACKING_STEP_FFNN = 1;
        public const int AUTHORIZATION_TRACKING_STEP_FISCAL = 2;
        public const int AUTHORIZATION_TRACKING_STEP_OPERATOR = 3;
        public const int AUTHORIZATION_TRACKING_STEP_SUPERVISOR = 4;

        public const string SERVICE_PAYMENT_DETAIL_DELAPAZ = " ENERGÍA ELÉCTRICA DELAPAZ";
        public const string SERVICE_PAYMENT_DETAIL_MOBILE_TELEPHONY = " MÓVIL";
        public const string SERVICE_PAYMENT_DETAIL_FIXED_TELEPHONY = " FIJA";
        public const string SERVICE_PAYMENT_DETAIL_SAGUAPAC = " SAGUAPAC";
        public const string SERVICE_PAYMENT_DETAIL_CRE = " CRE";
        public const string PAYMENT_PAYMENT_DETAIL_RUAT_PROPERTIES = " IMPUESTOS DE INMUEBLES";
        public const string PAYMENT_PAYMENT_DETAIL_VEHICLE = " IMPUESTOS DE VEHÍCULOS";
        public const string PAYMENT_PAYMENT_DETAIL_AFP = " APORTES A AFP";

        public const string PAYMENT_SERVICE_DELAPAZ = "PAGO DE SERVICIOS ENERGÍA ELÉCTRICA DELAPAZ";
        public const string PAYMENT_SERVICE_CRE = "PAGO DE SERVICIOS CRE";
        public const string PAYMENT_SERVICE_SAGUAPAC = "PAGO DE SERVICIOS SAGUAPAC";
        public const string PAYMENT_SERVICE_RUAT_PROPERTIES = "PAGO DE SERVICIOS IMPUESTOS DE INMUEBLES";
        public const string PAYMENT_SERVICE_RUAT_VEHICLE = "PAGO DE SERVICIOS IMPUESTOS DE VEHÍCULOS";

        public const string STATE_ACCESS_NUMBER_RESET = "R";
        public const string STATE_ACCESS_NUMBER_NEW = "N";
        public const string STATE_ACCESS_NUMBER_GENERATED = "G";
        public const string STATE_ACCESS_NUMBER_CHANGE = "C";
        public const string STATE_ACCESS_NUMBER_LOCKED = "B";
        public const string STATE_ACCESS_NUMBER_UNLOCKED = "D";

    }

    public enum TransferTypes
    {
        own,
        thirdParty,
        interbank
    }


}
