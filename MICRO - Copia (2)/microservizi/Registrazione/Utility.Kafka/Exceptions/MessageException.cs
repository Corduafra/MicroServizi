namespace UniprUtility.Kafka.Exceptions;

public class MessageException : ArgumentException {
    /// <inheritdoc/>
    public MessageException() : base() { }

    /// <inheritdoc/>
    public MessageException(string? message) : base(message) { }

    /// <inheritdoc/>
    public MessageException(string? message, Exception? innerException) : base(message, innerException) { }

    /// <inheritdoc/>
    public MessageException(string? message, string? paramName) : base(message, paramName) { }

    /// <inheritdoc/>
    public MessageException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException) { }
}
