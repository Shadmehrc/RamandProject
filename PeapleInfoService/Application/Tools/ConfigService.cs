using System;
using System.Linq;
using System.Threading.Tasks;
using Application.IRepository;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Application.Tools
{
    public class ConfigService
    {
        private readonly IConfigRepository _configRepository;
        private readonly ILogger<ConfigService> _logger;
        public ConfigService( ILogger<ConfigService> logger, IConfigRepository configRepository)
        {
            _logger = logger;
            _configRepository = configRepository;
        }

        public async Task<ConfigModel> GetConfigs()
        {
            try
            {
                var keyValueLLists = await _configRepository.GetAppConfig();
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