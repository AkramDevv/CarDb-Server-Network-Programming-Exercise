using System.Net.Sockets;
using System.Text.Json;
using ClientApp;

var client = new TcpClient();
client.Connect("127.0.0.1", 27001);

var stream = client.GetStream();
var bw = new BinaryWriter(stream);
var br = new BinaryReader(stream);

while (true)
{
    Console.WriteLine("Enter command (GET, POST, DELETE, PUT):");
    string text = Console.ReadLine()!.ToUpper();

    Command command = new();

    if (text == "POST" || text == "PUT")
    {
        Car car = new();
        Console.Write("Id: "); car.Id = int.Parse(Console.ReadLine()!);
        Console.Write("Brand: "); car.Brand = Console.ReadLine();
        Console.Write("Model: "); car.Model = Console.ReadLine();
        Console.Write("Year: "); car.Year = int.Parse(Console.ReadLine()!);

        command.Text = text;
        command.Param = JsonSerializer.Serialize(car);
    }
    else if (text == "DELETE")
    {
        Console.Write("ID to be deleted: ");
        int id = int.Parse(Console.ReadLine()!);
        command.Text = "DELETE";
        command.Param = id.ToString();
    }
    else if (text == "GET")
    {
        command.Text = "GET";
    }
    else
    {
        Console.WriteLine("Unknown command!");
        continue;
    }

    string json = JsonSerializer.Serialize(command);
    bw.Write(json);

    string response = br.ReadString();

    if (text == "GET")
    {
        var cars = JsonSerializer.Deserialize<List<Car>>(response)!;
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Id} | {car.Brand} | {car.Model} | {car.Year}");
        }
    }
    else
    {
        Console.WriteLine(response);
    }

    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
    Console.Clear();
}
