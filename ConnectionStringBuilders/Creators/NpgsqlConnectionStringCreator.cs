using System;
using System.Collections.Generic;
using System.Text;
using GRC.DatabaseUtils.ConnectionStringBuilders.Creators.Base;
using Npgsql;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Creators
{
    public class NpgsqlConnectionStringCreator: BaseConnectionStringCreator
    {
        public NpgsqlConnectionStringCreator(string server, string port, string username, string password) 
            : base(server, port, username, password)
        {

        }

        public override string CreateConnectionString(string name)
        {
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = Server,
                Username = Username,
                Password = Password,
                Database = name
            };

            if (int.TryParse(Port, out var port))
            {
                builder.Port = port;
            }

            return builder.ConnectionString;
        }
    }
}
