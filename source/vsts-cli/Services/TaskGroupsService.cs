using Microsoft.TeamFoundation.DistributedTask.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vsx.Extensions;

namespace vsx.Services
{
    public class TaskGroupsService : ITaskGroupsService
    {
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parserService;

        public TaskGroupsService(IConnectionService connectionService, IParserService parserService)
        {
            _connectionService = connectionService;
            _parserService = parserService;
        }

        public async Task<IList<TaskGroup>> GetTaskGroups()
        {
            var client = await _connectionService.GetTaskHttpClient();

            return await client.GetTaskGroupsAsync(_connectionService.Project);
        }

        public async Task<TaskGroup> GetTaskGroupById(string rawId)
        {
            var taskGroupId = rawId.EvaluateToGuid();
            var taskGroups = await GetTaskGroups();

            return taskGroups.Where(x => x.Id == taskGroupId).FirstOrDefault();
        }
    }
}
