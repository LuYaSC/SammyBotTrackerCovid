using System;
using System.Collections.Generic;
using TC.Core.Connectors.Models;
using TC.Core.Validation;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace TC.Core.Connectors
{
    public abstract class SFTPConnector<REQUEST, RESPONSE, CONNECTOR_RESPONSE> : FTPBaseConnector<REQUEST, RESPONSE, CONNECTOR_RESPONSE>
         where REQUEST : ProtocolsRequest
        where RESPONSE : class, new()
    {
        SftpClient client;
        EventLogGestorTranWriter logger = new EventLogGestorTranWriter();

        public SFTPConnector(REQUEST request, IValidator<REQUEST> validator) : base(request, validator)
        {
        }

        protected override void DisposeConnection()
        {
            if (client != null)
            {
                client.Disconnect();
                client.Dispose();
            }
        }

        private void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    prompt.Response = request.Password;
                }
            }
        }

        protected override bool OpenConnenction()
        {
            try
            {
                if (request.IsSpecial)
                {
                    KeyboardInteractiveAuthenticationMethod keybAuth = new KeyboardInteractiveAuthenticationMethod(request.UserName);
                    keybAuth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
                    ConnectionInfo conInfo = new ConnectionInfo(request.Host, request.Port, request.UserName, keybAuth);
                    client = new SftpClient(conInfo);
                }
                else
                {
                    client = new SftpClient(request.Host, request.Port, request.UserName, request.Password);
                }
                client.Connect();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Excepcion en la conexion a SFTP");
                this.Response.SetException(e);
                return false;
            }
            return client.IsConnected;
        }
    }
}
