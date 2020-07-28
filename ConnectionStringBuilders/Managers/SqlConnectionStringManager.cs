using System.Data.SqlClient;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers
{
    internal class SqlConnectionStringManager : BaseConnectionStringManager<System.Data.SqlClient.SqlConnectionStringBuilder>
    {
        public SqlConnectionStringManager(string connectionString) : base(
            new SqlConnectionStringBuilder(connectionString)) { }

        public override string GetPassword() => IntegratedSecurityEnabled() ? string.Empty : Builder.Password;

        public override void SetPassword(string newPassword) => Builder.Password = newPassword;

        public override void SetUser(string newUser) => Builder.UserID = newUser;

        protected override bool IntegratedSecurityEnabled() => Builder.IntegratedSecurity;
    }
}
