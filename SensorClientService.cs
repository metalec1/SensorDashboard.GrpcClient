using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using SensorDashboard;
//using SensorDashboard.Models;

namespace SensorDashboard.GrpcClient;

public class SensorClientService : IDisposable, ISensorClientService
{   
    private GrpcChannel _channel;
    private SensorService.SensorServiceClient _client;
    public event Action<SensorReading>? SensorReadingReceived;

    public SensorClientService(string grpcServerAddress = "http://localhost:5291")
    {
        _channel = GrpcChannel.ForAddress(grpcServerAddress);
        _client = new SensorService.SensorServiceClient(_channel);
    }

    public void StartListening()
    {
        
        var call = _client.SubscribeToSensor(new SensorUpdateRequest());
        _ = Task.Run (async() =>
        {
            await foreach (var update in call.ResponseStream.ReadAllAsync())
            {
                
                var reading = new SensorReading(
                    update.Timestamp.ToDateTime(),
                    update.Message,
                    update.Speed,
                    update.IsActive
                );
                Console.WriteLine(reading);
                SensorReadingReceived?.Invoke(reading);
                
            }
        });
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

}