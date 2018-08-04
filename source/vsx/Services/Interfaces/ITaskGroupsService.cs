using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Services
{
    public interface ITaskGroupsService
    {
        Task<IList<TaskGroup>> GetTaskGroups();

        Task<TaskGroup> GetTaskGroupById(Guid taskGroupId);

        Task<IList<TaskGroup>> SearchForTaskInTaskGroups(Guid taskId);
    }
}
