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
                //  x.UseSerilog();



                x.RunAsLocalSystem();
                x.SetDescription("");
                x.SetDisplayName("");
                x.SetServiceName("");


            });

            Environment.ExitCode = (int)Convert.ChangeType(host, host.GetTypeCode(), CultureInfo.InvariantCulture);



        }
    }
}
