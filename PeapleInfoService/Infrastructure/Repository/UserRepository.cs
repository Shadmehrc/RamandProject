using Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepository;
using Application.Query.GetUserList.GetUserListQuery;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["connection"];
        }

        public async Task<List<UserModel>> GetUsersList(GetUserListQuery model)
        {
            var result = new List<UserModel>();
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[GetUsersList]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Id", model.Id);
                    comm.Parameters.AddWithValue("@UserName", model.UserName);


                    var reader = await comm.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader);
                        }
                    }
                }
            }

            return result;

        }

        public async Task<RegisterUserResultModel> CreateUser(RegisterUserModel model)
        {
            var result = new RegisterUserResultModel();
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[CreateUser]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@UserName", model.UserName);
                    comm.Parameters.AddWithValue("@HashedPassword", model.HashedPassword);
                    comm.Parameters.AddWithValue("@FullName", model.FullName);
                    comm.Parameters.AddWithValue("@ProvinceTitle", model.ProvinceTitle);
                    comm.Parameters.AddWithValue("@Age", model.Age);


                    var reader = await comm.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader;
                        }
                    }
                }
            }
            return result;
        }
    }
}
