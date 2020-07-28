using System;

namespace GRC.DatabaseUtils
{
    public static class DatabaseManagersCreator
    {
        public static IDatabaseManager Create(string providerName, string connectionString)
        {
            switch (providerName)
            {
                case "sqlserver":
                    return new MssqlDatabaseManager(connectionString);
                case "postgresql":
                    return new NpgsqlDatabaseManager(connectionString);
                case "oracle":
                case "Адаптер ODBC RDBMS":
                case "sqlserverce":
                case "firebird":
                case "mysql":
                case "sqlite":
                default:
                    throw new NotSupportedException();

            }
        }
    }
}
