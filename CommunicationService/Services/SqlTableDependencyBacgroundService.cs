using Jaroszek.ProofOfConcept.CommunicationService.Interfaces;
using Jaroszek.ProofOfConcept.CommunicationService.Model;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace Jaroszek.ProofOfConcept.CommunicationService.Services
{
    public class SqlTableDependencyBacgroundService : IWindowsService
    {
        private SqlTableDependency<ReceivedRequest> sqlTableDependency;

        public SqlTableDependencyBacgroundService()
        {
            this.SqlTableDependencyBacgroundServiceAsync().Wait();
        }

        public async Task SqlTableDependencyBacgroundServiceAsync()
        {
            using (sqlTableDependency = new SqlTableDependency<ReceivedRequest>(Properties.Settings.Default.OfficeConnectionString, "ReceivedRequestBiling"))
            {
                sqlTableDependency.OnChanged += Changed;
                sqlTableDependency.OnError += TableDependency_OnError;
            }
        }

        public static void Changed(object sender, RecordChangedEventArgs<ReceivedRequest> e)
        {
            var changedEntity = e.Entity;

            Log.Logger = SetConfigurationLogger.GetLogger();
            Log.Logger.Write(LogEventLevel.Information, changedEntity.Request);

            //    Console.WriteLine("DML operation: " + e.ChangeType);
            //    Console.WriteLine("ID: " + changedEntity.Id);
            //    Console.WriteLine("Data: " + changedEntity.Data);
            //   Console.WriteLine("Request: " + changedEntity.Request);
        }
        private static void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.Error;
            Log.Logger = SetConfigurationLogger.GetLogger();
            Log.Logger.Write(LogEventLevel.Error, e.Message);
            Console.WriteLine(e.Message);
        }

        public void StartService()
        {
            Log.Logger = SetConfigurationLogger.GetLogger();
            Log.Logger.Write(LogEventLevel.Information, "Start Service");
            sqlTableDependency.Start();
        }

        public void StopService()
        {
            Log.Logger = SetConfigurationLogger.GetLogger();
            Log.Logger.Write(LogEventLevel.Information, "Stop Service");
            this.sqlTableDependency.Stop();
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}