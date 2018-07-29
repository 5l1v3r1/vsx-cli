using Microsoft.TeamFoundation.DistributedTask.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vsx.Services
{
    public interface ITaskService
    {
        Task<IList<TaskDefinition>> GetTasks();

        Task<TaskDefinition> GetTaskById(string id);
    }
}
