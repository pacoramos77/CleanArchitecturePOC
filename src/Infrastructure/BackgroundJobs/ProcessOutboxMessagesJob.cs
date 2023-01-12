using Core.Util;

using Infrastructure.Data;
using Infrastructure.Data.Outbox;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Polly;
using Polly.Retry;

using Quartz;

using SharedKernel.Domain;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly IPublisher _publisher;

    // private readonly ILogger _logger;

    public ProcessOutboxMessagesJob(AppDbContext dbContext, IPublisher publisher /*, ILogger logger*/)
    {
        _dbContext = dbContext;
        _publisher = publisher;

        // _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            DomainEventBase? domainEvent = null;

            try
            {
                domainEvent = DomainEventsSerializer.Deserialize(outboxMessage.Content);
            }
            catch (Exception ex)
            {
                outboxMessage.Error = ex.Message;
                LogSerializationError(outboxMessage, outboxMessage.Error);
                continue;
            }

            if (domainEvent is null)
            {
                outboxMessage.Error = "Serialization null result";
                LogSerializationError(outboxMessage, outboxMessage.Error);
                continue;
            }

            AsyncRetryPolicy policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, attemt => TimeSpan.FromMilliseconds(50 * attemt));

            PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                _publisher.Publish(domainEvent, context.CancellationToken));

            outboxMessage.Error = result.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = DateTimeProvider.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }

    private static void LogSerializationError(OutboxMessage outboxMessage, string errorMessage)
    {
        // _logger.LogError()
        Console.WriteLine($"{errorMessage}. Outbox message Id: {outboxMessage.Id}. {outboxMessage.Content}.");
    }
}
