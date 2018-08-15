using System;
using System.Collections.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;

namespace vsx.Services
{
    public class ReleaseDefinitionsService : IReleaseDefinitionsService
    {
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public ReleaseDefinitionsService(
            IConsole console,
            IConnectionService connectionService)
        {
            _console = console;
            _connectionService = connectionService;
        }

        public async Task<IList<ReleaseDefinition>> GetReleaseDefinitions()
        {
            _console.WriteLine($"Getting all release definitions under project: {_connectionService.Project}");

            var client = await _connectionService.GetReleaseHttpClient();

            return await client.GetReleaseDefinitionsAsync(project: _connectionService.Project, expand: ReleaseDefinitionExpands.Environments);
        }

        public async Task<ReleaseDefinition> GetReleaseDefinitionById(int definitionId)
        {
            var client = await _connectionService.GetReleaseHttpClient();

            return await client.GetReleaseDefinitionAsync(_connectionService.Project, definitionId);
        }

        public async Task<IList<ReleaseDefinition>> SearchForTaskInReleaseDefinitions(Guid taskId)
        {
            var listContainingSingleTask = new List<Guid> { taskId };
            return await SearchForTaskInReleaseDefinitions(listContainingSingleTask);
        }

        public async Task<IList<ReleaseDefinition>> SearchForTaskInReleaseDefinitions(IList<Guid> taskIds)
        {
            var client = await _connectionService.GetReleaseHttpClient();
            var definitions = await client.GetReleaseDefinitionsAsync(project: _connectionService.Project, expand: ReleaseDefinitionExpands.None);
            var definitionsContainingTask = new List<ReleaseDefinition>();

            if (definitions.Count < 100)
            {
                foreach (var definition in definitions)
                {
                    var expandedDefinition = await client.GetReleaseDefinitionAsync(_connectionService.Project, definition.Id);
                    if (DoesReleaseDefinitionContainsTask(expandedDefinition, taskIds)) definitionsContainingTask.Add(expandedDefinition);
                };
            }
            else
            {
                await definitions.ParallelForEachAsync(async definition =>
                {
                    var expandedDefinition = await client.GetReleaseDefinitionAsync(_connectionService.Project, definition.Id);
                    if (DoesReleaseDefinitionContainsTask(expandedDefinition, taskIds)) definitionsContainingTask.Add(expandedDefinition);
                },
                maxDegreeOfParalellism: 10);
            }

            return definitionsContainingTask;
        }

        private bool DoesReleaseDefinitionContainsTask(ReleaseDefinition definition, IList<Guid> taskIds)
        {
            var deployPhases = definition
                                .Environments
                                .SelectMany(environment => environment.DeployPhases)
                                .SelectMany(deployPhase => deployPhase.WorkflowTasks);

            foreach (var taskId in taskIds)
            {
                var task = deployPhases
                            .Where(t => t?.TaskId == taskId)
                            .FirstOrDefault();

                if (task != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
