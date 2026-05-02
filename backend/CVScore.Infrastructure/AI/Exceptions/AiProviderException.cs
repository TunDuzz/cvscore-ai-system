namespace CVScore.Infrastructure.AI.Exceptions;

public class AiProviderException : Exception
{
    public AiProviderException(string message)
        : base(message)
    {
    }

    public AiProviderException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
