using TC.Core.Connectors.Models;
using TC.Core.Validation;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TC.Core.Connectors
{
    public abstract class ResultStoreProcedureConnector<REQUEST, RESPONSE> : SqlBaseConnector<REQUEST, RESPONSE>
        where REQUEST : class, new()
        where RESPONSE : class, new()
    {
        public ResultStoreProcedureConnector(string connection, string procedure, REQUEST request, IValidator<REQUEST> validator)
            : base(connection, request, validator)
        {
            this.procedure = procedure;
        }

        private string procedure;

        protected abstract List<Parameter> parameters { get; }

        protected override DataRowCollection CallService()
        {
            DataTable Result = new DataTable();
            var sqlCommand = new SqlCommand(this.procedure, this.connection);
            SqlDataAdapter command = new SqlDataAdapter(sqlCommand);
            command.SelectCommand.CommandType = CommandType.StoredProcedure;
            command.SelectCommand.CommandTimeout = 600000;

            foreach (var parameter in this.parameters)
            {
                command.SelectCommand.Parameters.AddWithValue(parameter.Name, parameter.Value).Direction = parameter.Direction;
            }

            command.Fill(Result);

            return Result.Rows;
        }
    }
}