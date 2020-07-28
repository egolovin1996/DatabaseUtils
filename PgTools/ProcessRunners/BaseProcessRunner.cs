using System.Diagnostics;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools.ProcessRunners
{
    internal abstract class BaseProcessRunner
    {
        protected NpgsqlConnectionStringBuilder ConnectionStringBuilder;

        protected BaseProcessRunner(NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            ConnectionStringBuilder = connectionStringBuilder;
        }

        public void RunProcess(string command)
        {
            try
            {
                var info = PrepareAndGetStartInfo(command);
                using var process = System.Diagnostics.Process.Start(info);
                process.WaitForExit();
                process.Close();
            }
            finally
            {
                DeleteTempFiles();
            }
        }

        protected abstract ProcessStartInfo PrepareAndGetStartInfo(string command);

        protected abstract void DeleteTempFiles();
    }
}
