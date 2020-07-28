using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools
{
    public class PgRestoreExecutor : BaseToolExecutor
    {
        public PgRestoreExecutor(string toolPath, string backupsPath, NpgsqlConnectionStringBuilder connectionStringBuilder) : base(toolPath, backupsPath, connectionStringBuilder)
        {
        }

        protected override string GetToolCommand(string outFilePath)
        {
            // -c - удаляет (DROP) объекты базы данных, прежде чем пересоздавать их
            var result =
                $"pg_restore -c -h {ConnectionStringBuilder.Host} -p {ConnectionStringBuilder.Port}" +
                $" -d {ConnectionStringBuilder.Database} -U {ConnectionStringBuilder.Username} \"{outFilePath}\"";

            return result;
        }

        protected override void BeforeStartAction(string outFilePath)
        {
            
        }
    }
}
