using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools
{
    public class PgDumpExecutor : BaseToolExecutor
    {
        public PgDumpExecutor(string toolPath, string backupsPath, NpgsqlConnectionStringBuilder connectionStringBuilder) 
            : base(toolPath, backupsPath, connectionStringBuilder)
        {

        }

        protected override string GetToolCommand(string outFilePath)
        {
            // -Fc - сохранение в специальном формате pg_dump
            var result =
                $"pg_dump -Fc -h {ConnectionStringBuilder.Host} -p {ConnectionStringBuilder.Port}" +
                $" -d {ConnectionStringBuilder.Database} -U {ConnectionStringBuilder.Username} > {outFilePath}";

            return result;
        }

        protected override void BeforeStartAction(string outFilePath)
        {
            if (File.Exists(outFilePath))
            {
                File.Delete(outFilePath);
            }
        }
    }
}
