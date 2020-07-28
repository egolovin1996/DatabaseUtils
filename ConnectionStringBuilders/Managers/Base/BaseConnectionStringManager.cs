using System.Data.Common;
using GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Interfaces;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Managers.Base
{
    internal abstract class BaseConnectionStringManager<T> : IConnectionStringManager
        where T : DbConnectionStringBuilder
    {
        protected readonly T Builder;

        protected BaseConnectionStringManager() { }

        protected BaseConnectionStringManager(T builder)
        {
            Builder = builder;
        }

        public abstract string GetPassword();
        public abstract void SetPassword(string newPassword);
        public abstract void SetUser(string newUser);

        public string GetConnectionString() => Builder.ConnectionString;

        protected abstract bool IntegratedSecurityEnabled();
    }
}
