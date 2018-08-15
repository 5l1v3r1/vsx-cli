using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace vsx.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReleaseDefinitionsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<ReleaseDefinition>> GetReleaseDefinitions();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        Task<ReleaseDefinition> GetReleaseDefinitionById(int definitionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<IList<ReleaseDefinition>> SearchForTaskInReleaseDefinitions(Guid taskId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<IList<ReleaseDefinition>> SearchForTaskInReleaseDefinitions(IList<Guid> taskId);
    }
}
