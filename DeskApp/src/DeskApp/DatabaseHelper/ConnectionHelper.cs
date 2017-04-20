using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DatabaseHelper
{
    public enum DbConnetionType
    {
        Sqlite
    }

    public class ConnectionHelper
    {
        public DbConnection GetDbConnection(string connectionString,
                        DbConnetionType type)
        {
            switch (type)
            {
                case DbConnetionType.Sqlite:
                    return new SqliteConnection(connectionString);
                default:
                    return null;
            }
        }
    }
}
