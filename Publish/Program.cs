using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Configuración de conexión
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            DispatchConsumersAsync = true // Importante para versiones recientes
        };

        try
        {
            // 2. Conexión (versión compatible)
            using var connection = factory.CreateConnection(); // Método síncrono estándar

            // 3. Creación del canal
            using var channel = connection.CreateModel();

            // 4. Declaración de cola
            channel.QueueDeclare(
                queue: "mi_cola",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // 5. Creación y envío del mensaje
            string message = "Hola RabbitMQ!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "mi_cola",
                basicProperties: null,
                body: body);

            Console.WriteLine($" [x] Enviado: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}