using Serilog;

namespace Jaroszek.ProofOfConcept.CommunicationService
{
    public static class SetConfigurationLogger
    {
        public static ILogger GetLogger()
        {
            var loggerConfig = new LoggerConfiguration()
                //  .WriteTo.Console()
                .MinimumLevel.Verbose()
                //  .WriteTo.File("log.log")
                .WriteTo.Seq("http://localhost:5341");

            return loggerConfig.CreateLogger();
        }

    }
}