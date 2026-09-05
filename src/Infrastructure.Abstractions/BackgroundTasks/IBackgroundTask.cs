using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;

/// <summary>
/// Представляет асинхронную фоновую задачу
/// </summary>
/// <typeparam name="TArgs">Тип аргументов фоновой задачи</typeparam>
public interface IBackgroundTask<in TArgs>
{
    /// <summary>
    /// Запускает логику фоновой операции
    /// </summary>
    /// <param name="args">Аргументы фоновой задачи</param>
    /// <returns></returns>
    public Task ExecuteAsync(TArgs args);
}
