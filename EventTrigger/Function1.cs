using System;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PatientEventTrigger
{
    public class PatientFunction
    {
        private readonly ILogger<PatientFunction> _logger;

        public PatientFunction(ILogger<PatientFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(PatientFunction))]
        public void Run([EventHubTrigger("patient-events", Connection = "EventHubConnection")] EventData[] events)
        {
            // Iterate through each patient event received from the Event Hub
            foreach (EventData eventData in events)
            {
                // Log the patient event details
                string eventBody = eventData.Body.ToString();
                string contentType = eventData.ContentType;

                _logger.LogInformation("Received Patient Event: {body}", eventBody);
                _logger.LogInformation("Event Content-Type: {contentType}", contentType);

                // You can add additional processing logic here, such as parsing the event body
                // and saving patient information to a database or triggering other workflows.
            }
        }
    }
}