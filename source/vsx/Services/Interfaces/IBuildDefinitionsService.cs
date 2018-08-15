using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;

namespace vsx.Services
{
    /// <summary>
    /// Service to handle queries build definitions
    /// </summary>
    public interface IBuildDefinitionsService
    {
        /// <summary>
        /// Get all build definitions.
        /// </summary>
        /// <returns>A list of all build definitions.</returns>
        Task<IList<BuildDefinition>> GetBuildDefinitions();

        /// <summary>
        /// Get a single build definition by its id.
        /// </summary>
        /// <param name="definitionId">A valid build definition id.</param>
        /// <returns>A single build definition.</returns>
        Task<BuildDefinition> GetBuildDefinitionById(int definitionId);

        /// <summary>
        /// Get a single build definition by its name.
        /// </summary>
        /// <param name="definitionName">Name of the build definition.</param>
        /// <returns>A single build definition.</returns>
        Task<BuildDefinition> GetBuildDefinitionByName(string definitionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(Guid taskId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(IList<Guid> taskIds);
    }
}
