using System;
using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

/// <summary>
/// Предоставляет методы для управления очередью фоновых задач
/// </summary>
public interface IBackgroundTaskManager
{
    /// <summary>
    /// Ставит задачу в очередь на немедленное выполнение
    /// </summary>
    /// <typeparam name="TArgs">Тип аргументов фоновой задачи</typeparam>
    /// <param name="args">Аргументы фоновой задачи</param>
    /// <param name="priority">Приоритет выполнения фоновой задачи</param>
    /// <returns></returns>
    public Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundTaskPriority? priority = default)
        where TArgs : class;

    /// <summary>
    /// Планирует отложенное выполнение фоновой задачи через заданный промежуток времени
    /// </summary>
    /// <typeparam name="TArgs">Тип аргументов фоновой задачи</typeparam>
    /// <param name="args">Аргументы фоновой задачи</param>
    /// <param name="delay">Временная задержка перед постановкой задачи в очередь на выполнение</param>
    /// <param name="priority">Приоритет выполнения фоновой задачи</param>
    /// <returns></returns>
    public Task<string> ScheduleAsync<TArgs>(TArgs args, TimeSpan delay, BackgroundTaskPriority? priority = default)
        where TArgs : class;

    /// <summary>
    /// Планирует отложенное выполнение фоновой задачи на заданное время
    /// </summary>
    /// <typeparam name="TArgs">Тип аргументов фоновой задачи</typeparam>
    /// <param name="args">Аргументы фоновой задачи</param>
    /// <param name="enqueueAt">Точное время, когда задача должна быть поставлена в очередь на выполнение</param>
    /// <param name="priority">Приоритет выполнения фоновой задачи</param>
    /// <returns></returns>
    public Task<string> ScheduleAsync<TArgs>(TArgs args, DateTimeOffset enqueueAt, BackgroundTaskPriority? priority = default)
        where TArgs : class;
}
