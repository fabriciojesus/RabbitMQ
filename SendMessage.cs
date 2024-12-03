using RabbitMQ.Client;
using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Configurações de conexão com RabbitMQ
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",  // RabbitMQ rodando localmente
            UserName = "guest",     // Usuário padrão
            Password = "guest"      // Senha padrão
        };

        // Criando a conexão e o canal
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declarando uma nova fila
            string queueName = "nova_fila";
            channel.QueueDeclare(
                queue: FilaTest,     // Nome da fila
                durable: false,       // A fila não será persistente
                exclusive: false,     // Pode ser acessada por outras conexões
                autoDelete: false,    // Não será excluída automaticamente
                arguments: null);     // Sem argumentos adicionais

            // Mensagem a ser enviada
            string message = "Teste mensagem 1!";
            var body = Encoding.UTF8.GetBytes(message);

            // Publicando a mensagem na fila
            channel.BasicPublish(
                exchange: "",          // Usando o exchange padrão
                routingKey: FilaTest, // Nome da fila
                basicProperties: null, // Sem propriedades adicionais
                body: body);           // Corpo da mensagem

            Console.WriteLine($"Mensagem enviada: {message}");
        }

        Console.WriteLine("Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}
