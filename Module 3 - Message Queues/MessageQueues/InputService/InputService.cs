using System;
using System.IO;
using System.Messaging;
using System.Threading;

namespace InputService
{
    class InputService
    {
        const string InputServiceQueueName = @".\Private$\InputService";
        const string ServerQueueName = @".\Private$\CentralServer";
        const string PathToFolder = "../../InputServiceFolder";

        public void Start()
        {
            CreateMessageQueue();

            FileSystemWatcher watcher = new FileSystemWatcher(PathToFolder);

            watcher.Created += SendFile;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("InputService is working.");
        }

        private static void CreateMessageQueue()
        {
            if (!MessageQueue.Exists(InputServiceQueueName))
            {
                MessageQueue.Create(InputServiceQueueName);
            }
        }

        private void SendFile(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"New file detected: {e.Name}");

            using (MessageQueue serverQueue = new MessageQueue(ServerQueueName, QueueAccessMode.Send))
            using (MessageQueue inputServiceQueue = new MessageQueue(InputServiceQueueName, true))
            {
                var stopWorkEvent = new ManualResetEvent(false);

                inputServiceQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                inputServiceQueue.MessageReadPropertyFilter.CorrelationId = true;

                using (FileStream fileStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    Message message = new Message
                    {
                        BodyStream = fileStream,
                        Label = e.Name,
                        ResponseQueue = inputServiceQueue
                    };

                    serverQueue.Send(message);

                    string id = message.Id;
                    string correlationId = "";

                    while (id != correlationId)
                    {
                        var asyncReceive = inputServiceQueue.BeginPeek();

                        var res = WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent, asyncReceive.AsyncWaitHandle });
                        if (res == 0)
                        {
                            break;
                        }                           

                        message = inputServiceQueue.EndPeek(asyncReceive);

                        inputServiceQueue.ReceiveById(message.Id);
                        correlationId = message.CorrelationId;
                    }

                    Console.WriteLine($"Message from CentralServer: {message.Body}");
                }                
            }
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            PrintException(e.GetException());
        }            

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}
