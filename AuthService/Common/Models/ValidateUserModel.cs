using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Core.Models
{
    public class ValidateUserModel
    {
        public bool IsSuccess{ get; set; }
        public string ResultMessage { get; set; }
        public int?  UserId{ get; set; }


        public static implicit operator ValidateUserModel(SqlDataReader reader)
        {
            var result = new ValidateUserModel()
            {
                IsSuccess = Convert.ToBoolean(reader[nameof(IsSuccess)]),
                ResultMessage = reader[nameof(ResultMessage)].ToString()
            };
            if (reader[nameof(UserId)] != DBNull.Value)
                result.UserId = Convert.ToInt32(reader[nameof(UserId)]);
            return result;
        }

  
    }
}
