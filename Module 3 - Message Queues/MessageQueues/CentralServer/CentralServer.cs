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
            Console.WriteLine("CentralServer is working.");

            CreateMessageQueue();
            ProcessDocument();            
        }

        private static void CreateMessageQueue()
        {
            if (!MessageQueue.Exists(ServerQueueName)) {
                MessageQueue.Create(ServerQueueName, true);
            }
        }

        private static void ProcessDocument()
        {
            using (MessageQueue serverQueue = new MessageQueue(ServerQueueName))
            {
                while (true)
                {
                    MessageQueue responseQueue = null;

                    string id = string.Empty;
                    string fileName = string.Empty;

                    serverQueue.Formatter = new BinaryMessageFormatter();                   

                    using (MessageQueueTransaction transaction = new MessageQueueTransaction())
                    {
                        try
                        {
                            transaction.Begin();

                            Message message = serverQueue.Receive();

                            byte[] buffer = new byte[16 * 1024];
                            using (StreamWriter ms = new StreamWriter($"{path}/{message.Label}", true))
                            {
                                int read;

                                while ((read = message.BodyStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    ms.BaseStream.Write(buffer, 0, read);
                                }
                            }

                            responseQueue = message.ResponseQueue;
                            fileName = message.Label;
                            id = message.Id;

                            transaction.Commit();
                        }
                        catch(Exception exception)
                        {
                            transaction.Abort();

                            Console.WriteLine($"There was error while receiving messages: {exception.Message}");
                        }                                            
                    }

                    Console.WriteLine($"File recieved: {fileName}");                    

                    if (responseQueue != null)
                    {
                        responseQueue.Send(new Message($"File received: {fileName}") { CorrelationId = id });
                    }
                }
            }
        }
    }
}
