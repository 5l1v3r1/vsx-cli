using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using vsx.Extensions;

namespace vsx.Services
{
    public class TaskService : ITaskService
    {
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parserService;

        public TaskService(IConnectionService connectionService, IParserService parserService)
        {
            _connectionService = connectionService;
            _parserService = parserService;
        }

        public async Task<IList<TaskDefinition>> GetTasks()
        {
            var client = await _connectionService.GetTaskHttpClient();

            return await client.GetTaskDefinitionsAsync();
        }

        public async Task<TaskDefinition> GetTaskById(string rawId)
        {
            var taskId = rawId.EvaluateToGuid();
            var taskGroups = await GetTasks();

            return taskGroups.Where(x => x.Id == taskId).FirstOrDefault();
        }
    }
}
