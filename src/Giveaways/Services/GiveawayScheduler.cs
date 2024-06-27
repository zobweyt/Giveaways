using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Hangfire.States;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;

namespace Giveaways.Services;

/// <summary>
/// Represents a service responsible for scheduling giveaways.
/// </summary>
public class GiveawayScheduler
{
    private readonly IBackgroundJobClient _client;
    private readonly IMonitoringApi _monitor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GiveawayScheduler"/> class.
    /// </summary>
    /// <param name="client">The background job client.</param>
    public GiveawayScheduler(IBackgroundJobClient client)
    {
        _client = client;
        _monitor = JobStorage.Current.GetMonitoringApi();
    }

    /// <summary>
    /// Schedules a giveaway to expire at a specific date and time.
    /// </summary>
    /// <param name="messageId">The ID of the message associated with the giveaway.</param>
    /// <param name="expiresAt">The expiration date and time of the giveaway.</param>
    public void Schedule(ulong messageId, DateTime expiresAt)
        => _client.Schedule<GiveawayService>(service => service.ExpireAsync(messageId), expiresAt);

    /// <summary>
    /// Changes the state of a scheduled job associated with a giveaway.
    /// </summary>
    /// <param name="messageId">The ID of the message associated with the giveaway.</param>
    /// <param name="state">The new state to set for the job.</param>
    public void ChangeState(ulong messageId, IState state)
        => _client.ChangeState(GetScheduledJob(messageId).Key, state);

    /// <summary>
    /// Retrieves the scheduled job associated with a specific message ID.
    /// </summary>
    /// <param name="messageId">The ID of the message associated with the giveaway.</param>
    /// <returns>A key-value pair representing the scheduled job.</returns>
    public KeyValuePair<string, ScheduledJobDto?> GetScheduledJob(ulong messageId)
        => _monitor.ScheduledJobs(0, int.MaxValue).FirstOrDefault(j => (ulong)j.Value.Job.Args[0] == messageId);
}
