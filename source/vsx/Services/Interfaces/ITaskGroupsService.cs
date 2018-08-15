using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskGroupsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<TaskGroup>> GetTaskGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        Task<TaskGroup> GetTaskGroupById(Guid taskGroupId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<IList<TaskGroup>> SearchForTaskInTaskGroups(Guid taskId);
    }
}
