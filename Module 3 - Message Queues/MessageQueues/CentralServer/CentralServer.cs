using System;
using System.IO;
using System.Messaging;

namespace CentralServer
{
    static class CentralServer
    {
        const string ServerQueueName = @".\Private$\CentralServer";
        const string path = "../../CentralServerFolder";

        public static void Start()
        {
            CreateMessageQueue();
            ProcessDocument();

            Console.WriteLine("CentralServer is working.");
        }

        private static void CreateMessageQueue()
        {
            if(!MessageQueue.Exists(ServerQueueName)) {
                MessageQueue.Create(ServerQueueName);
            }
        }

        private static void ProcessDocument()
        {
            using (MessageQueue serverQueue = new MessageQueue(ServerQueueName))
            {
                while (true)
                {
                    Message message = serverQueue.Receive();
                    message.Formatter = new XmlMessageFormatter();

                    using (StreamReader reader = new StreamReader(message.BodyStream))
                    using (StreamWriter writer = new StreamWriter($"{path}/{message.Label}"))
                    {
                        writer.Write(reader.ReadToEnd());
                    }

                    Console.WriteLine($"Recieved: {message.Label}");

                    MessageQueue responseQueue = message.ResponseQueue;

                    if (responseQueue != null)
                    {
                        responseQueue.Send(new Message($"File received: {message.Label}") { CorrelationId = message.Id });
                    }
                }
            }
        }
    }
}
