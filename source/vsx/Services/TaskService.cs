﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Services
{
    public class TaskService : ITaskService
    {
        private readonly IConnectionService _connectionService;

        public TaskService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IList<TaskDefinition>> GetTasks()
        {
            var client = await _connectionService.GetTaskHttpClient();

            return await client.GetTaskDefinitionsAsync();
        }

        public async Task<TaskDefinition> GetTaskById(Guid taskId)
        {
            var taskGroups = await GetTasks();

            return taskGroups.Where(x => x.Id == taskId).FirstOrDefault();
        }
    }
}
