using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DatabaseHelper
{
    public class ParameterHelper
    {
        public DbParameter GetParameter(string parameterName, object value,
            DbConnetionType databaseType)
        {
            switch (databaseType)
            {
                case DbConnetionType.Sqlite:
                    var sqliteParameter = new SqliteParameter
                    {
                        ParameterName = parameterName,
                        Value = value
                    };
                    return sqliteParameter;
                default:
                    return null;
            }
        }
    }
}
