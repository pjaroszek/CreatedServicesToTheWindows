using Jaroszek.ProofOfConcept.CommunicationService.Interfaces;
using Jaroszek.ProofOfConcept.CommunicationService.Model;
using Serilog;
using TableDependency.SqlClient;

namespace Jaroszek.ProofOfConcept.CommunicationService.Services
{
    public class SqlTableDependencyBacgroundService : IWindowsService
    {
        private readonly SqlTableDependency<ReceivedRequest> sqlTableDependency;

        public SqlTableDependencyBacgroundService()
        {
            sqlTableDependency =
                new SqlTableDependency<ReceivedRequest>(Properties.Settings.Default.OfficeConnectionString,
                    "ReceivedRequestBiling");
            sqlTableDependency.OnChanged += (sender, e) =>
            {
                Log.Information("Request: {request}", e.Entity.Request);
            };
            sqlTableDependency.OnError += (sender, e) =>
            {
                Log.Error("Błąd: {error}", e.Error.Message);
            };
        }

        public void StartService()
        {
            Log.Information("Start Service");
            sqlTableDependency.Start();
        }

        public void StopService()
        {
            Log.Information("Stop Service");
            this.sqlTableDependency.Stop();
        }

        public void Dispose()
        {
            this.sqlTableDependency.Dispose();
        }
    }
}