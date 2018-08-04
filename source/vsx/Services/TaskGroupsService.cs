using Microsoft.TeamFoundation.DistributedTask.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vsx.Extensions;

namespace vsx.Services
{
    public class TaskGroupsService : ITaskGroupsService
    {
        private readonly IConnectionService _connectionService;

        public TaskGroupsService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IList<TaskGroup>> GetTaskGroups()
        {
            var client = await _connectionService.GetTaskHttpClient();

            return await client.GetTaskGroupsAsync(_connectionService.Project);
        }

        public async Task<TaskGroup> GetTaskGroupById(Guid taskGroupId)
        {
            var taskGroups = await GetTaskGroups();

            return taskGroups.Where(x => x.Id == taskGroupId).FirstOrDefault();
        }

        public async Task<IList<TaskGroup>> SearchForTaskInTaskGroups(Guid taskId)
        {
            var taskGroups = await GetTaskGroups();
            var taskGroupsContainingTask = new List<TaskGroup>();

            foreach (var taskGroup in taskGroups)
            {
                if (DoesTaskGroupContainsTask(taskGroup, taskId)) taskGroupsContainingTask.Add(taskGroup);
            }

            return taskGroupsContainingTask;
        }

        private bool DoesTaskGroupContainsTask(TaskGroup taskGroup, Guid taskId)
        {
            if ((taskGroup.Tasks != null) && (taskGroup.Tasks.Any()))
            {
                foreach (var task in taskGroup.Tasks)
                {
                    if (task.Task.Id == taskId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
