using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using GRC.DatabaseUtils.PgTools.ProcessRunners;
using Microsoft.VisualBasic.FileIO;
using Npgsql;

namespace GRC.DatabaseUtils.PgTools
{
    public abstract class BaseToolExecutor
    {
        private readonly string _toolPath;
        private readonly string _backupsPath;

        protected NpgsqlConnectionStringBuilder ConnectionStringBuilder;

        protected BaseToolExecutor(string toolPath, string backupsPath, NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            _toolPath = toolPath;
            _backupsPath = backupsPath;
            ConnectionStringBuilder = connectionStringBuilder;
        }

        public bool TryExecute()
        {
            var outFilePath = $@"{_backupsPath}{ConnectionStringBuilder.Database}.dump";
            var command = GetToolCommand(outFilePath);
            BeforeStartAction(outFilePath);

            var runner = GetProcessRunner();
            runner.RunProcess(command);

            return true;
        }

        protected abstract string GetToolCommand(string outFilePath);

        protected abstract void BeforeStartAction(string outFilePath);

        private BaseProcessRunner GetProcessRunner()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsProcessRunner(ConnectionStringBuilder, _toolPath);
            }

            return new LinuxProcessRunner(ConnectionStringBuilder);
        }
    }
}
