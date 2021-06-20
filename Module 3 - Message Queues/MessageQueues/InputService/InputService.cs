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
                MessageQueue.Create(InputServiceQueueName, true);
            }
        }

        private void SendFile(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"New file detected: {e.Name}");

            using (MessageQueue serverQueue = new MessageQueue(ServerQueueName, QueueAccessMode.Send))
            using (MessageQueue inputServiceQueue = new MessageQueue(InputServiceQueueName, true))
            {
                var stopWorkEvent = new ManualResetEvent(false);

                serverQueue.Formatter = new BinaryMessageFormatter();

                inputServiceQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                inputServiceQueue.MessageReadPropertyFilter.CorrelationId = true;

                long fileLen = new FileInfo(e.FullPath).Length;
                long totalRead = 0;

                int partSize = 3 * 1024 * 1024;

                string id = string.Empty;

                using (FileStream fileStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read))
                {
                    using (var transaction = new MessageQueueTransaction())
                    {
                        transaction.Begin();

                        while (totalRead < fileLen)
                        {
                            var part = new byte[fileLen - totalRead < partSize ? fileLen - totalRead : partSize];

                            totalRead += fileStream.Read(part, 0, part.Length);

                            Message message = new Message
                            {
                                BodyStream = new MemoryStream(part),
                                Label = e.Name,
                                ResponseQueue = inputServiceQueue
                            };

                            id = message.Id;

                            serverQueue.Send(message, transaction);
                        }

                        transaction.Commit();
                    }

                    
                    string correlationId = string.Empty;

                    Message responseMessage = null;

                    while (id != correlationId)
                    {
                        var asyncReceive = inputServiceQueue.BeginPeek();

                        var res = WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent, asyncReceive.AsyncWaitHandle });
                        if (res == 0)
                        {
                            break;
                        }

                        responseMessage = inputServiceQueue.EndPeek(asyncReceive);

                        inputServiceQueue.ReceiveById(responseMessage.Id);
                        correlationId = responseMessage.CorrelationId;
                    }


                    if(responseMessage != null)
                    {
                        Console.WriteLine($"Message from CentralServer: {responseMessage.Body}");
                    }                    
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
