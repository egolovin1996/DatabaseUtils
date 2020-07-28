using System;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers
{
    internal class OdbcConnectionStringManager: BaseConnectionStringManager<System.Data.Odbc.OdbcConnectionStringBuilder>
    {
        public OdbcConnectionStringManager(string connectionString) : base(
            new System.Data.Odbc.OdbcConnectionStringBuilder(connectionString)) { }

        public override string GetPassword()
        {
            if (IntegratedSecurityEnabled())
            {
                return string.Empty;
            }

            var success = Builder.TryGetValue("pwd", out object value);
            if (!success) throw new ArgumentNullException("\"pwd\""); // выше осуществляется перехват этого исключения
            return (string) value;
        }

        public override void SetPassword(string newPassword) => Builder["pwd"] = newPassword;

        public override void SetUser(string newUser) => Builder["uid"] = newUser;

        protected override bool IntegratedSecurityEnabled()
        {
            var success = Builder.TryGetValue("trusted_connection", out object value);
            return success && ((string) value).Equals("yes", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
