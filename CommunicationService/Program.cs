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
                    x.UseSerilog();

                    //    x.DependsOn("CommunicationService");
                    //   x.DependsOnEventLog();




                    x.Service<SqlTableDependencyBacgroundService>(s =>
                        {
                            s.ConstructUsing(name => new SqlTableDependencyBacgroundService());
                            s.WhenStarted(tc => tc.StartService());
                            s.WhenStopped(tc => tc.StopService());
                        });


                    x.RunAsLocalSystem();
                    x.SetDescription("CommunicationService");
                    x.SetDisplayName("CommunicationService");
                    x.SetServiceName("CommunicationService");


                });

            Environment.ExitCode = (int)Convert.ChangeType(host, host.GetTypeCode(), CultureInfo.InvariantCulture);



        }



    }
}
