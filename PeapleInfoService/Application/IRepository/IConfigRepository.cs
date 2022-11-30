using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Application.IRepository
{
    public interface IConfigRepository
    {
        Task<List<ConfigKeyValueList>> GetAppConfig();
        
    }
}
