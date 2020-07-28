using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools.ProcessRunners
{
    internal class WindowsProcessRunner: BaseProcessRunner
    {
        private readonly string _toolPath;

        private string _batFilePath;
        private string _passFilePath;

        public WindowsProcessRunner(NpgsqlConnectionStringBuilder connectionStringBuilder, string toolPath) 
            : base(connectionStringBuilder)
        {
            _toolPath = toolPath;
        }

        protected override ProcessStartInfo PrepareAndGetStartInfo(string command)
        {
            _passFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".conf");
            _batFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bat");

            var passFileContent = GetPassFileContent();
            var batchContent = $"set PGPASSFILE={_passFilePath}\n\"{_toolPath}\"{command}";

            File.WriteAllText(_batFilePath, batchContent, Encoding.ASCII);
            File.WriteAllText(_passFilePath, passFileContent, Encoding.ASCII);

            var result = new ProcessStartInfo(_batFilePath)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            return result;
        }

        protected override void DeleteTempFiles()
        {
            if (File.Exists(_batFilePath))
                File.Delete(_batFilePath);

            if (File.Exists(_passFilePath))
                File.Delete(_passFilePath);
        }

        private string GetPassFileContent()
        {
            var result =
                $"{ConnectionStringBuilder.Host}:{ConnectionStringBuilder.Port}" +
                $":{ConnectionStringBuilder.Database}:{ConnectionStringBuilder.Username}" +
                $":{ConnectionStringBuilder.Password}";

            return result;
        }
    }
}
