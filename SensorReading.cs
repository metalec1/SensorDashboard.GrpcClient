using System;

namespace SensorDashboard.GrpcClient;

public record SensorReading
{   
    public DateTime Timestamp { get; init; }
    public string Message { get; init; }
    public double Speed { get; init; }
    public bool IsActive { get; init; }
    
    public SensorReading(DateTime timestamp,string message, double speed, bool isActive)
    {
        Timestamp = timestamp;
        Message = message;
        Speed = speed;
        IsActive = isActive;
    }
}