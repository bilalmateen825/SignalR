// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;

Thread.Sleep(1000);

Console.WriteLine("Client UP");
var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/chatHub")
    .Build();


connection.StartAsync().Wait();

//Receive event
connection.On("ReceiveMessage", (string stUserName, string message) =>
{
    Console.WriteLine(string.Format("{0} : {1}", stUserName, message));
});

//Send message to SignalR hub
connection.InvokeCoreAsync("SendMessageAsync", args: new[] { "Bilal", "Hello from Console Client" });

#region Maintain MultiCast message

#endregion

Console.ReadLine();