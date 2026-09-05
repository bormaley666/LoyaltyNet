namespace LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

/// <summary>
/// Предоставляет методы для регистрации и планирования периодического выполнения фоновых задач
/// </summary>
public interface IBackgroundRecurringTaskScheduler
{
    /// <summary>
    /// Планирует периодическое выполнение фоновой задачи в соответствии с указанным CRON-выражением
    /// </summary>
    /// <typeparam name="TTask">Тип фоновой задачи, реализующей интерфейс <see cref="IBackgroundRecurringTask"/></typeparam>
    /// <param name="cronExpression">Строковое представление CRON-выражения</param>
    public void Schedule<TTask>(string cronExpression)
        where TTask : IBackgroundRecurringTask;
}
