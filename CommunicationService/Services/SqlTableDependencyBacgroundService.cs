using Jaroszek.ProofOfConcept.CommunicationService.Interfaces;
using Jaroszek.ProofOfConcept.CommunicationService.Model;
using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace Jaroszek.ProofOfConcept.CommunicationService.Services
{
    public class SqlTableDependencyBacgroundService : IWindowsService
    {

        public SqlTableDependencyBacgroundService()
        {
            using (var dep = new SqlTableDependency<ReceivedRequest>(Properties.Settings.Default.OfficeConnectionString, "ReceivedRequest"))
            {
                dep.OnChanged += Changed;
                dep.OnError += TableDependency_OnError;

                dep.Start();
                Console.WriteLine("Press a key to exit");
                Console.ReadKey();

                dep.Stop();
            }
        }

        public static void Changed(object sender, RecordChangedEventArgs<ReceivedRequest> e)
        {
            var changedEntity = e.Entity;

            Console.WriteLine("DML operation: " + e.ChangeType);
            Console.WriteLine("ID: " + changedEntity.Id);
            Console.WriteLine("Data: " + changedEntity.Data);
            Console.WriteLine("Request: " + changedEntity.Request);
        }
        private static void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.Error;
            Console.WriteLine(e.Message);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public void StartService()
        {
            //   throw new NotImplementedException();
        }

        public void StopService()
        {
            //  throw new NotImplementedException();
        }
    }
}