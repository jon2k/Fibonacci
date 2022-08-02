// See https://aka.ms/new-console-template for more information


using EasyNetQ;

var connStr = "host=localhost:5672;username=guest;password=guest";

using (var bus = RabbitHutch.CreateBus(connStr))
{
    var input = "";
    Console.WriteLine("Please enter a message.'Quit' to quit.");
    while ((input = Console.ReadLine()) != "Quit")
    {
        bus.PubSub.Publish(new MessageResponseFib(){});
    }
}
Console.WriteLine("Hello, World!");
public class MessageResponseFib
{
    public int Number { get; set; }
    public int CurrentNumber { get; set; }
    public long Sum { get; set; } 
}