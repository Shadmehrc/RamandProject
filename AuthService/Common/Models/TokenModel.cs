using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TokenModel
    {
        public int UserId{ get; set; }
        public string UserName { get; set; }

        public string HashedToken{ get; set; }
        public string HashedRefreshToken{ get; set; }
        public DateTime TokenExp{ get; set; }
        public DateTime RefreshTokenExp{ get; set; }

        public static implicit operator TokenModel(SqlDataReader reader)
        {
            var model = new TokenModel();

            if (reader[nameof(HashedToken)] != DBNull.Value)
                model.HashedToken = reader[nameof(HashedToken)].ToString();

            if (reader[nameof(HashedRefreshToken)] != DBNull.Value)
                model.HashedRefreshToken = reader[nameof(HashedRefreshToken)].ToString();


            if (reader[nameof(UserName)] != DBNull.Value)
                model.UserName = reader[nameof(UserName)].ToString();

            if (reader[nameof(TokenExp)] != DBNull.Value)
                model.TokenExp = Convert.ToDateTime(reader[nameof(TokenExp)]);


            if (reader[nameof(RefreshTokenExp)] != DBNull.Value)
                model.RefreshTokenExp =Convert.ToDateTime(reader[nameof(RefreshTokenExp)]);

            if (reader[nameof(UserId)] != DBNull.Value)
                model.UserId = Convert.ToInt32(reader[nameof(UserId)]);
            return model;
        }
    }
}
