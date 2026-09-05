namespace LoyaltyNet.Domain.References.Mccs;

/// <summary>
/// Параметры фильтрации MCC-кодов
/// </summary>
public sealed class MccFilter
{
    public static MccFilter Empty() => new(null, null);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="query">MCC-код или наименование</param>
    /// <param name="withCategory"></param>
    public MccFilter(string? query, bool? withCategory)
    {
        WithCategory = withCategory ?? false;
        Query = query;
    }

    /// <summary>
    /// MCC-код или наименование
    /// </summary>
    public string? Query { get; }

    public bool WithCategory { get; }
}
