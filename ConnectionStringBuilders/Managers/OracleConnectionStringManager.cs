using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers
{
    internal class OracleConnectionStringManager : BaseConnectionStringManager<
        Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder>
    {
        public OracleConnectionStringManager(string connectionString) : base(
            new Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder(connectionString)) { }

        public override string GetPassword() => IntegratedSecurityEnabled() ? string.Empty : Builder.Password;

        public override void SetPassword(string newPassword) => Builder.Password = newPassword;

        public override void SetUser(string newUser) => Builder.UserID = newUser;

        protected override bool IntegratedSecurityEnabled() => Builder.UserID.Equals("/");
    }
}
