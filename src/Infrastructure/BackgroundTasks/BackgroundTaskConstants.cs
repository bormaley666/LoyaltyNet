using System;
using System.Collections.Generic;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

/// <summary>
/// Содержит константы и вспомогательные методы для работы с очередями фоновых задач
/// </summary>
internal static class BackgroundTaskConstants
{
    private const string High = "priority-queue-01";
    private const string Normal = "priority-queue-02";
    private const string Low = "priority-queue-03";

    private static readonly Dictionary<BackgroundTaskPriority, string> _priorityMapping =
        new()
        {
            [BackgroundTaskPriority.High] = High,
            [BackgroundTaskPriority.Normal] = Normal,
            [BackgroundTaskPriority.Low] = Low
        };

    /// <summary>
    /// Идентификатор очереди для периодических (повторяющихся) фоновых задач
    /// </summary>
    public const string RecurringQueue = "recurring-queue";

    /// <summary>
    /// Идентификаторы очередей, используемых для обработки фоновых задач
    /// </summary>
    public static string[] Queues { get; } = [High, Normal, Low];

    /// <summary>
    /// Возвращает идентификатор очереди, соответствующий указанному приоритету задачи
    /// </summary>
    /// <param name="priority"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetQueue(BackgroundTaskPriority priority) =>
        _priorityMapping.TryGetValue(priority, out var queue)
            ? queue
            : throw new ArgumentOutOfRangeException(nameof(priority), priority, $"Unsupported priority: {priority}");
}
