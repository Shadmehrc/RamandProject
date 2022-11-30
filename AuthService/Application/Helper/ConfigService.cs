using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Application.Helper
{
    public class ConfigService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<ConfigService> _logger;
        public ConfigService(IAuthenticationRepository authenticationRepository, ILogger<ConfigService> logger)
        {
            _authenticationRepository = authenticationRepository;
            _logger = logger;
        }

        public async Task<ConfigModel> GetConfigs()
        {
            try
            {
                var keyValueLLists = await _authenticationRepository.GetAppConfig();
                var output = new ConfigModel();
                foreach (var item in output.GetType().GetProperties())
                {
                     var itName = item.Name;

                    var value = keyValueLLists.FirstOrDefault(x => x.Key.ToString().ToLower() == itName.ToLower())?.Value;
                    item.SetValue(output, value, null);
                }
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ConfigModel();
            }
        }
    }
}
