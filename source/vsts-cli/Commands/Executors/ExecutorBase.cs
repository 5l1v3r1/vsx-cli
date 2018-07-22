using McMaster.Extensions.CommandLineUtils;

namespace vsx.Commands
{
    public abstract class ExecutorBase
    {
        [Option]
        public string AccountName { get; set; }

        [Option]
        public string PersonalAccessToken { get; set; }
    }
}
