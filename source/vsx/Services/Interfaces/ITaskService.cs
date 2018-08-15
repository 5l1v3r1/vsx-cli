using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

namespace vsx.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<TaskDefinition>> GetTasks();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<TaskDefinition> GetTaskById(Guid taskId);
    }
}
