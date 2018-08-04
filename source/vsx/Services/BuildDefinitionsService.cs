using System;
using System.Collections.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code;
using Microsoft.VisualStudio.Services.WebApi;
using vsx.Extensions;

namespace vsx.Services
{
    public class BuildDefinitionsService : IBuildDefinitionsService
    {
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parserService;

        public BuildDefinitionsService(
            IConsole console, 
            IConnectionService connectionService, 
            IParserService parserService)
        {
            _console = console;
            _connectionService = connectionService;
            _parserService = parserService;
        }

        public async Task<IList<BuildDefinition>> GetBuildDefinitions()
        {
            _console.WriteLine($"Getting all build definitions under project: {_connectionService.Project}");

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

            _console.WriteLine($"{buildDefinitions.Count} build definition(s) found!");

            return buildDefinitions;
        }

        public async Task<BuildDefinition> GetBuildDefinitionById(int definitionId)
        {
            var definitions = await GetBuildDefinitions();

            _console.WriteLine($"Getting build definition with id: {definitionId}");

            return definitions.Where(x => x.Id == definitionId).FirstOrDefault();
        }

        public async Task<BuildDefinition> GetBuildDefinitionByName(string definitionName)
        {
            var definitions = await GetBuildDefinitions();

            return definitions.Where(x => x.Name == definitionName).FirstOrDefault();
        }

        public async Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(Guid taskId)
        {
            _console.WriteLine($"Searching build definitions for task with id: {taskId}");

            var definitions = await GetBuildDefinitions();
            var definitionsContainingTask = new List<BuildDefinition>();

            _console.WriteLine($"Processing build definitions...");

            if (definitions.Count < 400)
            {
                foreach (var definition in definitions)
                {
                    _console.WriteLine($"Processing build definition: {definition.Name} ({definition.Id})");
                    await ProcessSingleDefinition(definition, taskId, definitionsContainingTask);
                };
            }
            else
            {
                await definitions.ParallelForEachAsync(async definition =>
                {
                    _console.WriteLine($"Processing build definition: {definition.Name} ({definition.Id})");
                    await ProcessSingleDefinition(definition, taskId, definitionsContainingTask);
                },
                maxDegreeOfParalellism: 10);
            }

            return definitionsContainingTask;
        }

        public Task<IList<BuildDefinition>> SearchForTaskInBuildDefinitions(Guid[] taskIds)
        {
            throw new NotImplementedException();
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
            var searchedTask = designerProcess.Phases
                .SelectMany(phase => phase.Steps)
                .Where(step => step.TaskDefinition.Id == taskId)
                .FirstOrDefault();

            return (searchedTask != null) ? true : false;
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
