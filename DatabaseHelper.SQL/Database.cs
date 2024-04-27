using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DatabaseHelper.SQL
{
    public sealed class Database
    {
        private readonly string _connectionString;
        private CommandType commandType;

        public Database(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        public SqlCommand GetCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlCommand command = GetConnection().CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Parameters.AddRange(parameters);
            return command;
        }

        public SqlCommand GetCommand(string commandText, params SqlParameter[] parameters) =>
            GetCommand(commandText, CommandType.Text, parameters);

        public int EcexuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = GetCommand(commandText, commandType, parameters))
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string commandText, CommandType commandtype, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = GetCommand(commandText, commandType, parameters))
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = GetCommand(commandText, commandType, parameters);
            connection.Open();
            return command.ExecuteReader();
        }

    }
}
