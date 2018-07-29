using Microsoft.TeamFoundation.DistributedTask.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace vsx.Services
{
    public interface ITaskGroupsService
    {
        Task<IList<TaskGroup>> GetTaskGroups();

        Task<TaskGroup> GetTaskGroupById(string id);
    }
}
