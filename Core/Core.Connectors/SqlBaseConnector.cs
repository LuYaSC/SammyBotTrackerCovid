using TC.Core.Validation;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TC.Core.Connectors
{
    public abstract class SqlBaseConnector<REQUEST, RESPONSE> : BaseConnector<REQUEST, RESPONSE>
        where REQUEST: class, new()
        where RESPONSE : class, new()
    {
        public SqlBaseConnector(string stringConnection, REQUEST request, IValidator<REQUEST> validator)
            : base(request, validator)
        {
            this.stringConnection = stringConnection;
        }

        private string stringConnection;

        protected SqlConnection connection;

        protected override void BeforeProcess()
        {
            
        }

        protected override void DisposeConnection()
        {
            if (this.connection != null)
            {
                this.connection.Close();
            }

            SqlConnection.ClearAllPools();
        }

        protected override bool OpenConnenction()
        {
            try
            {
                this.connection = new SqlConnection(this.stringConnection);
                this.connection.Open();
                return true;
            }
            catch (Exception e)
            {
                this.Response.SetException(e);                
                return false;
            }
        }

        protected override void Process()
        {
            try
            {
                var responseService = this.CallService();
                this.Response.Body = this.Mapping(responseService);
            }
            catch (Exception e)
            {
                this.Response.SetException(e);
            }
        }

        protected abstract DataRowCollection CallService();

        protected abstract RESPONSE Mapping(DataRowCollection responseService);
    }
}
