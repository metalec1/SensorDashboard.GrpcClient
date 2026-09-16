using System;


namespace SensorDashboard.GrpcClient;

public interface ISensorClientService
{
    public event Action<SensorReading>? SensorReadingReceived;
    public void StartListening();
}