using System.Data;

namespace DataAccess.Core.DbLib
{
    public static class DbUtils
    {
        public static IDataParameter CreateParameter(IDbCommand command, string name, DbType type, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value;
            return parameter;
        }

        public static void AddParameter(this IDbCommand command, string name, DbType type, object value)
        {
            var parameter = CreateParameter(command, name, type, value);
            command.Parameters.Add(parameter);
        }

        public static DataSet ExecuteDataSet(IDbCommand command, IDbDataAdapter adapter)
        {
            var ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            return ds;
        }
    }
}
