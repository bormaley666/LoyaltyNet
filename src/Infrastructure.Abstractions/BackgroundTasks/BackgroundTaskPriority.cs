namespace LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

/// <summary>
/// Определяет уровень приоритета выполнения фоновой задачи
/// </summary>
public enum BackgroundTaskPriority
{
    /// <summary>
    /// Низкий приоритет
    /// </summary>
    Low,

    /// <summary>
    /// Обычный (средний) приоритет
    /// </summary>
    Normal,

    /// <summary>
    /// Высокий приоритет
    /// </summary>
    High
}
