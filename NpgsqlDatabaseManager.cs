using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using GRC.DatabaseUtils.PgTools;
using Npgsql;

namespace GRC.DatabaseUtils
{
    public class NpgsqlDatabaseManager : BaseDatabaseManager<NpgsqlConnectionStringBuilder, NpgsqlConnection>
    {
        public NpgsqlDatabaseManager(string connectionString) : base(
            new NpgsqlConnectionStringBuilder(connectionString))
        {
            
        }

        protected override string ExtractBaseName()
        {
            return ConnectionStringBuilder.Database;
        }

        protected override string GetCheckExistenceCommand(string databaseName)
        {
            return $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
        }

        protected override NpgsqlConnection GetConnection()
        {
            var builder = new NpgsqlConnectionStringBuilder(ConnectionStringBuilder.ToString());
            builder.Database = null;
            return new NpgsqlConnection(builder.ToString());
        }

        protected override void DisconnectUsers(NpgsqlConnection connection, string databaseName)
        {
            // Source: https://dba.stackexchange.com/questions/11893/force-drop-db-while-others-may-be-connected
            // Make sure no one can connect to this database
            var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                $"UPDATE pg_database SET datallowconn = 'false' WHERE datname = '{databaseName}'";
            updateCommand.ExecuteNonQuery();

            // Force disconnection of all clients connected to this database
            var disconnectCommand = connection.CreateCommand();
            disconnectCommand.CommandText =
                $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{databaseName}'";
            disconnectCommand.ExecuteNonQuery();
        }

        public override bool TryCreateBackup(string backupsPath, string toolsPath)
        {
            var pgDumpExecutor = new PgDumpExecutor(toolsPath, backupsPath, ConnectionStringBuilder);
            return pgDumpExecutor.TryExecute();
        }

        public override bool TryRestoreBackup(string backupsPath, string toolsPath)
        {
            var pgRestoreExecutor = new PgRestoreExecutor(toolsPath, backupsPath, ConnectionStringBuilder);
            return pgRestoreExecutor.TryExecute();
        }
    }
}
