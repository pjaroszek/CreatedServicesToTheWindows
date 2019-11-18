using System;

namespace Jaroszek.ProofOfConcept.CommunicationService.Model
{
    public class ReceivedRequest
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Request { get; set; }

    }
}