namespace staff_management.apis.services;
public class CustomLogger<T>
{
  private readonly ILogger<T> _logger;
  private string? _customMessage;

  public CustomLogger(ILogger<T> logger)
  {
    _logger = logger;
  }


  public void SetCustomMessage(string customMessage)
  {
    _customMessage = customMessage;
  }

  public void AppendCustomMessage(string newMessage)
  {
    _customMessage += newMessage;
  }
  public void Log(LogLevel level, string message, Exception? exception = null)
  {
    var formattedMessage = string.IsNullOrEmpty(_customMessage)
        ? message
        : $"{message} | {_customMessage}";

    if (exception == null)
    {
      _logger.Log(level, formattedMessage);
    }
    else
    {
      _logger.Log(level, exception, formattedMessage);
    }
  }
}
