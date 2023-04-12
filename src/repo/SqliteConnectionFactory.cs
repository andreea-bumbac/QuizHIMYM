using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.repo
{
    internal class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string?> props)
        {
            var connectionString = props["ConnectionString"];
            Console.WriteLine("SQLite ---Opening connection at ... {0}", connectionString);
            return new SqliteConnection(connectionString);
        }
    }
}
