using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ConfigKeyValueList
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static implicit operator ConfigKeyValueList(SqlDataReader reader)
        {
            return new ConfigKeyValueList()
            {
                Key = reader[nameof(Key)].ToString(),
                Value = reader[nameof(Value)].ToString(),
            };
        }
    }
}
