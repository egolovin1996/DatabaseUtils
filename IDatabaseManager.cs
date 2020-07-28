using System;
using System.Collections.Generic;
using System.Text;

namespace GRC.DatabaseUtils
{
    public interface IDatabaseManager
    {
        bool TryCreateDatabase();
        bool TryDeleteDatabase();
        bool TryCreateBackup(string backupsPath, string toolsPath);
        bool TryRestoreBackup(string backupsPath, string toolsPath);
    }
}
