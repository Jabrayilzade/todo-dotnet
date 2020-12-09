using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWish.Services
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection();
    }
}