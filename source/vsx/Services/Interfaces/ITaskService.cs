using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Services
{
    public interface ITaskService
    {
        Task<IList<TaskDefinition>> GetTasks();

        Task<TaskDefinition> GetTaskById(Guid taskId);
    }
}
