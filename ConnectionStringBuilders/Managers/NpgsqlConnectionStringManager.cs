using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base;
using Npgsql;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers
{
    internal class NpgsqlConnectionStringManager : BaseConnectionStringManager<Npgsql.NpgsqlConnectionStringBuilder>
    {
        public NpgsqlConnectionStringManager(string connectionString) : base(
            new NpgsqlConnectionStringBuilder(connectionString)) { }

        public override string GetPassword() => Builder.Password;

        public override void SetPassword(string newPassword) => Builder.Password = newPassword;

        public override void SetUser(string newUser) => Builder.Username = newUser;

        protected override bool IntegratedSecurityEnabled() => Builder.IntegratedSecurity;
    }
}
