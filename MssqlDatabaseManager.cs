

using System.Data.SqlClient;

namespace GRC.DatabaseUtils
{
    public class MssqlDatabaseManager : BaseDatabaseManager<SqlConnectionStringBuilder, SqlConnection>
    {
        public MssqlDatabaseManager(string connectionString) : base(
            new SqlConnectionStringBuilder(connectionString))
        {

        }

        public override bool TryCreateBackup(string path, string toolsPath)
        {
            throw new System.NotImplementedException("Создание бэкапов в MsSql не реализовано");
        }

        public override bool TryRestoreBackup(string path, string toolsPath)
        {
            throw new System.NotImplementedException("Восстановление бэкапов в Ms Sql не реализовано");
        }

        protected override string ExtractBaseName()
        {
            return ConnectionStringBuilder.InitialCatalog;
        }

        protected override string GetCheckExistenceCommand(string databaseName)
        {
            return $"SELECT 1 database_id FROM sys.databases WHERE Name  = '{databaseName}'";
        }

        protected override SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionStringBuilder.ToString()) {InitialCatalog = null};
            return new SqlConnection(builder.ToString());
        }

        protected override void DisconnectUsers(SqlConnection connection, string databaseName)
        {
            // Todo: добавить логику для force drop 
        }
    }
}
