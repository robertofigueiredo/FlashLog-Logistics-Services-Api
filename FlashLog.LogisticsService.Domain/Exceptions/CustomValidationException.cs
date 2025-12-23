namespace FlashLog.LogisticsService.Domain.Exceptions;

public class CustomValidationException(string message, IEnumerable<string> errors) : Exception(message) 
{
    public IEnumerable<string> Errors { get; } = errors;
}