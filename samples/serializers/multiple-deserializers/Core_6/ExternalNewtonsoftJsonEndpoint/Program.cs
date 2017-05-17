﻿using System;
using System.Threading.Tasks;
using NServiceBus;

static class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.MultipleDeserializers.ExternalNewtonsoftJsonEndpoint";
        #region configExternalNewtonsoftJson
        var endpointConfiguration = new EndpointConfiguration("Samples.MultipleDeserializers.ExternalNewtonsoftJsonEndpoint");
        var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        serialization.ContentTypeKey("NewtonsoftJson");
        endpointConfiguration.RegisterOutgoingMessageLogger();

        #endregion
        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.UseTransport<LearningTransport>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        var message = MesasgeBuilder.BuildMessage();
        await endpointInstance.Send("Samples.MultipleDeserializers.ReceivingEndpoint", message)
            .ConfigureAwait(false);
        Console.WriteLine("Order Sent");
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

}