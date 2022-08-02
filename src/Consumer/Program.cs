// See https://aka.ms/new-console-template for more information

using EasyNetQ;

var connStr = "host=localhost:5672;username=guest;password=guest";

using (var bus = RabbitHutch.CreateBus(connStr))
{
    bus.PubSub.Subscribe<MessageResponseFib>("test", HandleTextMessage);

    Console.WriteLine("Listening for messages. Hit <return> to quit.");
    Console.ReadLine();
}

Console.WriteLine("Hello, World!");


void HandleTextMessage(MessageResponseFib textMessage)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Got message: {0}", textMessage.Number);
    Console.ResetColor();
}
