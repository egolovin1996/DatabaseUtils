using System;
using System.Collections.Generic;
using System.Text;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Creators.Base
{
    public abstract class BaseConnectionStringCreator
    {
        protected readonly string Server;
        protected readonly string Port;
        protected readonly string Username;
        protected readonly string Password;

        protected BaseConnectionStringCreator(string server, string port, string username, string password)
        {
            Server = server;
            Port = port;
            Username = username;
            Password = password;
        }

        public abstract string CreateConnectionString(string name);
    }
}
