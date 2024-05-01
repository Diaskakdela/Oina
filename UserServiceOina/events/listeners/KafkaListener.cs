using UserServiceOina.kafka;
using UserServiceOina.model;
using UserServiceOina.service;

namespace UserServiceOina.events.listeners;

public class KafkaListener
{
    private readonly IUserService _userService;
    private readonly KafkaProducer _kafkaProducer;
    private readonly ILogger<KafkaListener> _logger;
    private const string Topic = "user-registrations";

    public KafkaListener(IUserService userService, KafkaProducer kafkaProducer, ILogger<KafkaListener> logger)
    {
        _kafkaProducer = kafkaProducer;
        _userService = userService;
        _logger = logger;
        _userService.UserRegistered += HandleUserRegistered;
    }

    public void SubscribeToEvents()
    {
        _userService.UserRegistered += HandleUserRegistered;
        _logger.LogInformation("Subscribed to UserRegistered event.");
    }
    private async void HandleUserRegistered(object? sender, UserRegisteredEventArgs e)
    {
        var userDetailsMessage = new UserDetailsMessage(e.User);
        _logger.LogInformation($"Sending user registration info to Kafka: {userDetailsMessage}");
        try
        {
            await _kafkaProducer.SendMessageAsync(Topic, userDetailsMessage);
            _logger.LogInformation("Message successfully sent to Kafka.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send message to Kafka: {ex.Message}");
        }
    }
}