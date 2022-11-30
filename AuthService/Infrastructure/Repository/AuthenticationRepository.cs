using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepositories;
using Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _connectionString;

        public AuthenticationRepository(IConfiguration configuration)
        {
            _connectionString = configuration["connection"];
        }

        public async Task<ValidateUserModel> ValidateUser(LoginUserModel model)
        {
            var result = new ValidateUserModel();
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[ValidateUser]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@UserName", model.UserName);
                    comm.Parameters.AddWithValue("@HashedPassword", model.HashedPassword);


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
            return  result;
        }

        public async Task<bool> DeleteToken(int userId)
        {
            var isSuccess = false;
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[DeleteToken]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@UserId",userId);


                    var reader = await comm.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            isSuccess = Convert.ToBoolean(reader["IsSuccess"]);
                        }
                    }
                }
            }
            return isSuccess;
        }

        public async Task<bool> SaveToken(SaveTokenModel model)
        {
            var isSuccess = false;
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[AddToken]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@UserId", model.UserId);
                    comm.Parameters.AddWithValue("@HashedToken", model.HashedToken);
                    comm.Parameters.AddWithValue("@HashedRefreshToken", model.HashedRefreshToken);
                    comm.Parameters.AddWithValue("@TokenExp", model.TokenExp);
                    comm.Parameters.AddWithValue("@RefreshTokenExp", model.RefreshTokenExp);


                    var reader = await comm.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            isSuccess = Convert.ToBoolean(reader["IsSuccess"]);
                        }
                    }
                }
            }

            return true;
        }

        public async Task<TokenModel> FindRefreshToken(string refreshToken)
        {
            var result = new TokenModel();
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[FindRefreshToken]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@refreshToken", refreshToken);


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

        public async Task<List<ConfigKeyValueList>> GetAppConfig()
        {
            var result = new List<ConfigKeyValueList>();
            await using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await using (var comm = new SqlCommand("[dbo].[GetAppConfig]", conn))
                {
                    comm.CommandTimeout = int.MaxValue;
                    comm.CommandType = CommandType.StoredProcedure;

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
    }
}
