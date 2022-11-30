using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProvinceTitle { get; set; }
        public DateTime CreateDate { get; set; }

        public static implicit operator UserModel(SqlDataReader reader)
        {
           return new UserModel()
            {
                Id = Convert.ToInt32(reader[nameof(Id)]),
                Age = Convert.ToInt32(reader[nameof(Age)]),
                UserName = reader[nameof(UserName)].ToString(),
                FullName = reader[nameof(FullName)].ToString(),
                ProvinceTitle = reader[nameof(ProvinceTitle)].ToString(),
                CreateDate =Convert.ToDateTime(reader[nameof(CreateDate)]) ,
            };
        }
    }
}
