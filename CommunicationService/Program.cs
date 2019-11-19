using Jaroszek.ProofOfConcept.CommunicationService.Services;
using System;
using System.Globalization;
using Topshelf;

namespace Jaroszek.ProofOfConcept.CommunicationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.Run(
                x =>
                {
                    x.DependsOnMsSql();
                    x.UseSerilog(SetConfigurationLogger.GetLogger());

                    //    x.DependsOn("CommunicationService");
                    //   x.DependsOnEventLog();


                    x.Service<SqlTableDependencyBacgroundService>(s =>
                        {
                            s.ConstructUsing(name => new SqlTableDependencyBacgroundService());
                            s.WhenStarted(tc => tc.StartService());
                            s.WhenStopped(tc => tc.StopService());
                        });


                    x.RunAsLocalSystem();
                    x.SetDescription("AAa_CommunicationService");
                    x.SetDisplayName("AAa_CommunicationService");
                    x.SetServiceName("AAa_CommunicationService");


                });

            Environment.ExitCode = (int)Convert.ChangeType(host, host.GetTypeCode(), CultureInfo.InvariantCulture);



        }



    }
}
