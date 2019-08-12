using Microsoft.Extensions.Logging;

namespace Iwannago.Services
{
    class RunnerService
    {
        private readonly ILogger<RunnerService> _logger;

        public RunnerService(ILogger<RunnerService> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogInformation("This is an informational log message");
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }
    }
}
