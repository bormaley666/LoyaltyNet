namespace LoyaltyNet.Infrastructure.Abstractions.Caching;

/// <summary>
/// Кэш
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// Запрос на получение объекта, хранимого в кэше по указанному строковому ключу
    /// </summary>
    /// <typeparam name="T">Тип кэшированного объекта</typeparam>
    /// <param name="key">Строковое представление ключа</param>
    /// <param name="value">При успешном выполнении, содержит запись извлеченную из кэша</param>
    /// <returns></returns>
    public bool TryGet<T>(string key, out T? value);

    /// <summary>
    /// Добавляет объект в кэш, если ключ еще не существует
    /// </summary>
    /// <param name="cacheKey">Ключ кэша, содержащий составную информацию о ключе и параметрах хранения</param>
    /// <param name="value">Объект, который требуется поместить в кэш</param>
    /// <returns></returns>
    public bool Add(CacheKey cacheKey, object value);

    /// <summary>
    /// Помещает объект в кэш, перезаписывая существующее значение, если ключ уже присутствует
    /// </summary>
    /// <param name="cacheKey">Ключ кэша, содержащий составную информацию о ключе и параметрах хранения</param>
    /// <param name="value">Объект, который требуется поместить или обновить в кэше</param>
    public void Put(CacheKey cacheKey, object value);

    /// <summary>
    /// Удаляет запись из кэша по точному совпадению ключа
    /// </summary>
    /// <param name="key">Строковое представление ключа</param>
    public void Remove(string key);

    /// <summary>
    /// Удаляет все записи из кэша, ключи которых соответствуют префиксу
    /// </summary>
    /// <param name="prefix">Префикс ключа</param>
    public void RemoveByPrefix(string prefix);

    /// <summary>
    /// Полностью очищает всё содержимое кэша, удаляя все существующие записи
    /// </summary>
    public void Clear();
}
