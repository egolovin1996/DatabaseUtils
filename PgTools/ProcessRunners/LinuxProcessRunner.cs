using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools.ProcessRunners
{
    internal class LinuxProcessRunner: BaseProcessRunner
    {
        private string _shFilePath;

        public LinuxProcessRunner(NpgsqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
        {

        }

        protected override ProcessStartInfo PrepareAndGetStartInfo(string command)
        {
            _shFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".sh");
            var batchContent = $"export PGPASSWORD=\"{ConnectionStringBuilder.Password}\"\n{command}";
            File.WriteAllText(_shFilePath, batchContent, Encoding.ASCII);

            var result = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = _shFilePath,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            return result;
        }

        protected override void DeleteTempFiles()
        {
            if (File.Exists(_shFilePath))
                File.Delete(_shFilePath);
        }
    }
}
