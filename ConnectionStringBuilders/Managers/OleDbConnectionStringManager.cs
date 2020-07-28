using System;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers
{
    internal class OleDbConnectionStringManager: BaseConnectionStringManager<System.Data.OleDb.OleDbConnectionStringBuilder>
    {
        public OleDbConnectionStringManager(string connectionString) : base(
            new System.Data.OleDb.OleDbConnectionStringBuilder(connectionString)) { }

        public override string GetPassword() => IntegratedSecurityEnabled() ? string.Empty : (string) Builder["Password"];

        public override void SetPassword(string newPassword) => Builder["Password"] = newPassword;

        public override void SetUser(string newUser) => Builder["User ID"] = newUser;

        protected override bool IntegratedSecurityEnabled()
        {
            var success = Builder.TryGetValue("integrated security", out object value);
            return success && ((string) value).Equals("sspi", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
