using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TodoWish.Services;

namespace TodoWish.Services
{
    public class DapperDbConnection : IDbConnectionFactory
    {
        public DapperDbConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IDbConnection CreateDbConnection()
        {
            string connectionName = Configuration.GetConnectionString("Todo");
            IDbConnection connection = new NpgsqlConnection(connectionName);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            connection.Open();
            return connection;
        }
    }
}
