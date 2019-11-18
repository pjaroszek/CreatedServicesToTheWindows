using System;

namespace Jaroszek.ProofOfConcept.CommunicationService.Interfaces
{
    public interface IWindowsService : IDisposable
    {
        void StartService();
        void StopService();

    }
}