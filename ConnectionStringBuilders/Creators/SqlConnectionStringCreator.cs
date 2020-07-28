using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using GRC.DatabaseUtils.ConnectionStringBuilders.Creators.Base;

namespace GRC.DatabaseUtils.ConnectionStringBuilders.Creators
{
    public class SqlConnectionStringCreator: BaseConnectionStringCreator
    {
        public SqlConnectionStringCreator(string server, string port, string username, string password) 
            : base(server, port, username, password)
        {

        }

        public override string CreateConnectionString(string name)
        {
            var dataSource = string.IsNullOrEmpty(Port) ? Server : $"{Server},{Port}";
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource,
                UserID = Username,
                Password = Password,
                InitialCatalog = name
            };

            return builder.ConnectionString;
        }
    }
}
