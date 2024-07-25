using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:6969/chatHub")
    .Build();

Console.Write("Input name: ");
string name = Console.ReadLine();

await connection.StartAsync();
Console.WriteLine("Connected!");

await connection.InvokeAsync("JoinRoom", name);

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

var sendTask = Task.Run(async () =>
{
    while (true)
    {
        var message = Console.ReadLine();

        if (message == "exit")
        {
            break;
        }

        await connection.InvokeAsync("SendMessage", name, message);
    }
});

await sendTask;
