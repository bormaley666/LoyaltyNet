using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

/// <summary>
/// Фоновая задача выполняемая по расписанию
/// </summary>
public interface IBackgroundRecurringTask
{
    /// <summary>
    /// Запускает логику фоновой операции
    /// </summary>
    /// <returns></returns>
    public Task ExecuteAsync();
}