using System;
using System.Collections.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code;
using Microsoft.VisualStudio.Services.WebApi;
using vsx.Extensions;

namespace vsx.Services
{
    public class BuildDefinitionsService : IBuildDefinitionsService
    {
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parserService;

        public BuildDefinitionsService(IConnectionService connectionService, IParserService parserService)
        {
            _connectionService = connectionService;
            _parserService = parserService;
        }

        public async Task<IList<BuildDefinition>> GetBuildDefinitions()
        {
            List<BuildDefinition> buildDefinitions = new List<BuildDefinition>();
            string continuationToken = null;

            using (var client = await _connectionService.GetBuildHttpClient())
            {
                do
                {
                    IPagedList<BuildDefinition> buildDefinitionsPage = await client.GetFullDefinitionsAsync2(
                        project: _connectionService.Project,
                        continuationToken: continuationToken);

                    buildDefinitions.AddRange(buildDefinitionsPage);
                    continuationToken = buildDefinitionsPage.ContinuationToken;

                } while (!string.IsNullOrEmpty(continuationToken));
            }

            return buildDefinitions;
        }

        public async Task<BuildDefinition> GetBuildDefinitionById(string rawId)
        {
            var definitionId = rawId.EvaluateToId();
            var definitions = await GetBuildDefinitions();

            return definitions.Where(x => x.Id == definitionId).FirstOrDefault();
        }

        public async Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(string taskIdentifier)
        {
            var taskId = taskIdentifier.EvaluateToGuid();

            var definitions = await GetBuildDefinitions();
            var definitionsContainingTask = new List<BuildDefinition>();

            if (definitions.Count < 400)
            {
                foreach (var definition in definitions)
                {
                    await ProcessSingleDefinition(definition, taskId, definitionsContainingTask);
                };
            }
            else
            {
                await definitions.ParallelForEachAsync(async definition =>
                {
                    await ProcessSingleDefinition(definition, taskId, definitionsContainingTask);
                },
                maxDegreeOfParalellism: 10);
            }

            return definitionsContainingTask;
        }

        private async Task ProcessSingleDefinition(BuildDefinition definition, Guid taskId, IList<BuildDefinition> definitionsContainingTask)
        {
            switch (definition.Process.Type)
            {
                case ProcessType.Designer:
                    if (SarchForTaskInDesignerProcess(taskId, definition.Process as DesignerProcess)) definitionsContainingTask.Add(definition);
                    break;
                case ProcessType.Yaml:
                    if (await SearchForTaskInYamlDefinition(taskId, definition.Process as YamlProcess, definition.Repository.Id)) definitionsContainingTask.Add(definition);
                    break;
            }
        }

        private bool SarchForTaskInDesignerProcess(Guid taskId, DesignerProcess designerProcess)
        {
            if ((designerProcess.Phases != null) && (designerProcess.Phases.Any()))
            {
                foreach (var phase in designerProcess.Phases)
                {
                    if ((phase.Steps != null) && (phase.Steps.Any()))
                    {
                        foreach (var step in phase.Steps)
                        {
                            if (step.TaskDefinition.Id == taskId)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private async Task<bool> SearchForTaskInYamlDefinition(Guid taskId, YamlProcess yamlProcess, string repositoryId)
        {
            if (string.IsNullOrEmpty(yamlProcess.YamlFilename))
            {
                return false;
            }

            var safeYamlFileName = GetYamlFileName(yamlProcess.YamlFilename);

            var searchResults = await CodeSearchForYamlFile(safeYamlFileName);

            string yamlFileContent = default;
            if (searchResults.Count() > 0)
            {
                yamlFileContent = await GetYamlFileFromRepository(searchResults, repositoryId);

                if (string.IsNullOrEmpty(yamlFileContent))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return DoesYamlContainsTask(taskId, yamlFileContent) ? true : false;
        }

        private string GetYamlFileName(string yamlFilename)
        {
            return ((yamlFilename).Contains(@"/")) ? _parserService.ParseYamlFileName(yamlFilename) : yamlFilename;
        }

        private async Task<IEnumerable<CodeResult>> CodeSearchForYamlFile(string yamlFileName)
        {
            var searchClient = await _connectionService.GetSearchHttpClient();

            var codeSearchRequest = new CodeSearchRequest
            {
                Filters = default,
                SearchText = yamlFileName,
                Skip = 0,
                Top = 1000,
            };

            return (await searchClient.FetchCodeSearchResultsAsync(codeSearchRequest)).Results;
        }

        private async Task<string> GetYamlFileFromRepository(IEnumerable<CodeResult> searchResults, string buildRepositoryId)
        {
            var result = searchResults.Where(x => x.Repository.Id == buildRepositoryId).FirstOrDefault();

            var gitClient = await _connectionService.GetGitHttpClient();
            if (result != null)
            {
                var gitItem = await gitClient.GetItemAsync(result.Repository.Id, result.Path, includeContent: true);

                return gitItem.Content;
            }

            return string.Empty;
        }

        private bool DoesYamlContainsTask(Guid taskId, string parsedYamlFile)
            => (parsedYamlFile.Contains(taskId.ToString())) ? true : false;
    }
}
