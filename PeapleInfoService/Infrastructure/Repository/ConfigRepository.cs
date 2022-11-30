using Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly string _connectionString;

        public ConfigRepository(IConfiguration configuration)
        {
            _connectionString = configuration["connection"];
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
