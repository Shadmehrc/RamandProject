using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class RegisterUserResultModel
    {
        public bool IsSuccess{ get; set; }
        public string ResultMessage{ get; set; }

        public static implicit operator RegisterUserResultModel(SqlDataReader reader)
        {
            return new RegisterUserResultModel()
            {
                IsSuccess = Convert.ToBoolean(reader[nameof(IsSuccess)]),
                ResultMessage = reader[nameof(ResultMessage)].ToString()
            };
        }
    }
}
