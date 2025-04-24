using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using ServerApp;

var ip = IPAddress.Any;
var port = 27001;

var listener = new TcpListener(ip, port);
listener.Start();

Console.WriteLine("Server started...");

var methods = new CarMethods();

while (true)
{
    var client = listener.AcceptTcpClient();
    Console.WriteLine("Client connected!");

    var stream = client.GetStream();
    var br = new BinaryReader(stream);
    var bw = new BinaryWriter(stream);

    while (true)
    {
        try
        {
            var json = br.ReadString();
            var command = JsonSerializer.Deserialize<Command>(json);

            string response = "";

            if (command!.Text == "GET")
            {
                var cars = methods.GetAllCars();
                response = JsonSerializer.Serialize(cars);
            }
            else if (command.Text == "POST")
            {
                var car = JsonSerializer.Deserialize<Car>(command.Param!);
                response = methods.AddCar(car!);
            }
            else if (command.Text == "DELETE")
            {
                int id = int.Parse(command.Param!);
                response = methods.DeleteCar(id);
            }
            else if (command.Text == "PUT")
            {
                var car = JsonSerializer.Deserialize<Car>(command.Param!);
                response = methods.UpdateCar(car!);
            }
            else
            {
                response = "Unknown command";
            }
            bw.Write(response);
        }
        catch (Exception ex)
        {
            bw.Write("Error: " + ex.Message);
        }
    }

}
