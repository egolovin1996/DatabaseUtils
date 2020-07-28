using System;
using System.Data.Common;

namespace GRC.DatabaseUtils
{
    public abstract class BaseDatabaseManager<TBuilder, TConnection> : IDatabaseManager
        where TBuilder : DbConnectionStringBuilder where TConnection : DbConnection
    {
        protected TBuilder ConnectionStringBuilder;

        protected BaseDatabaseManager(TBuilder connectionStringBuilder)
        {
            ConnectionStringBuilder = connectionStringBuilder;
        }

        public bool TryCreateDatabase()
        {
            var databaseName = ExtractBaseName();
            using var connection = GetConnection();
            connection.Open();
            
            if (CheckDbExistence(connection, databaseName)) return false;

            var createCommand = connection.CreateCommand();
            createCommand.CommandText = $"CREATE DATABASE \"{databaseName}\"";
            createCommand.ExecuteNonQuery();

            return true;
        }

        public bool TryDeleteDatabase()
        {
            var databaseName = ExtractBaseName();
            using var connection = GetConnection();
            connection.Open();

            if (!CheckDbExistence(connection, databaseName)) return false;

            DisconnectUsers(connection, databaseName);

            var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = $"DROP DATABASE \"{databaseName}\"";
            deleteCommand.ExecuteNonQuery();

            return true;
        }

        public abstract bool TryCreateBackup(string backupsPath, string toolsPath);

        public abstract bool TryRestoreBackup(string backupsPath, string toolsPath);

        private bool CheckDbExistence(TConnection connection, string databaseName)
        {
            var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = GetCheckExistenceCommand(databaseName);
            return checkCommand.ExecuteScalar() != null;
        }

        protected abstract string ExtractBaseName();

        protected abstract string GetCheckExistenceCommand(string databaseName);

        protected abstract TConnection GetConnection();

        protected abstract void DisconnectUsers(TConnection connection, string databaseName);
    }
}
