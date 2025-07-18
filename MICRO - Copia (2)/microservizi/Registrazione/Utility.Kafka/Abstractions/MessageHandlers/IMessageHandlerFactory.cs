﻿namespace UniprUtility.Kafka.Abstractions.MessageHandlers;

public interface IMessageHandlerFactory<TKey, TValue>
    where TKey : class
    where TValue : class
{
    IMessageHandler<TKey, TValue> Create(string topic, IServiceProvider serviceProvider);
}
