using System;
using GRC.DatabaseUtils.ConnectionStringBuilders.Creators;
using GRC.DatabaseUtils.ConnectionStringBuilders.Creators.Base;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Interfaces;

namespace GRC.DatabaseUtils.ConnectionStringBuilders
{
    public static class ManagersFactory
    {
        public static IConnectionStringManager CreateBuilder(string providerName, string connectionString)
        {
            try
            {
                switch (providerName)
                {
                    case "sqlserver":
                        return new SqlConnectionStringManager(connectionString);
                    case "postgresql":
                    case "Адаптер Npgsql OLEDB RDBMS":
                        return new NpgsqlConnectionStringManager(connectionString);
                    case "oracle":
                    case "Адаптер Oracle OLEDB RDBMS":
                        return new OracleConnectionStringManager(connectionString);
                    case "Адаптер OLEDB RDBMS":
                        return new OleDbConnectionStringManager(connectionString);
                    case "Адаптер ODBC RDBMS":
                        return new OdbcConnectionStringManager(connectionString);
                    case "sqlserverce":
                    case "firebird":
                    case "mysql":
                    case "sqlite":
                    default:
                        throw new NotSupportedException($"Для адаптера \"{providerName}\" не определён decryptor.");

                }

            }
            catch (Exception e)
            {
                throw new Exception($"Строка соединения '{connectionString}' не является валидной", e);
            }
        }

        public static BaseConnectionStringCreator CreateCreator(string providerName, string server, string port, string username, string password)
        {
            switch (providerName)
            {
                case "sqlserver":
                    return new SqlConnectionStringCreator(server, port, username, password);
                case "postgresql":
                    return new NpgsqlConnectionStringCreator(server, port, username, password);
                default:
                    throw new NotSupportedException($"Провайдер \"{providerName}\" не поддерживается.");
            }
        }
    }
}
