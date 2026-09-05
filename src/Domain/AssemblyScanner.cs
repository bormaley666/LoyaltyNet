using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LoyaltyNet.Domain;

public static class AssemblyScanner
{
    private static readonly Dictionary<string, Assembly> _cachedAssemblies = new(StringComparer.InvariantCultureIgnoreCase);
    private static readonly object _lock = new();

    private static readonly string[] _assemblyPrefixesToSkip =
    [
        "System", "Microsoft", "mscorlib", "netstandard"
    ];

    public static IEnumerable<Type> GetImplementationsOf<T>(IEnumerable<Assembly>? assembliesToScan = null)
    {
        return GetImplementationsOf(typeof(T), assembliesToScan);
    }

    public static IEnumerable<Type> GetImplementationsOf(Type assignTypeFrom, IEnumerable<Assembly>? assembliesToScan = null)
    {
        ArgumentNullException.ThrowIfNull(assignTypeFrom);

        var types = new List<Type>();

        foreach (var assembly in assembliesToScan ?? Assemblies)
        {
            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (IsConcreteClass(type) && IsAssignableFrom(assignTypeFrom, type))
                    {
                        types.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var type in ex.Types.Where(e => e is not null))
                {
                    if (IsConcreteClass(type!) && IsAssignableFrom(assignTypeFrom, type!))
                    {
                        types.Add(type!);
                    }
                }
            }
        }

        return types;
    }

    private static bool IsAssignableFrom(Type assignTypeFrom, Type type) =>
        assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom));

    private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((_, _) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                {
                    continue;
                }

                if (genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                {
                    return true;
                }
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsConcreteClass(Type type) => type.IsClass && !type.IsAbstract && !type.IsInterface;

    private static IEnumerable<Assembly> Assemblies
    {
        get
        {
            if (_cachedAssemblies.Count == 0)
            {
                lock (_lock)
                {
                    if (_cachedAssemblies.Count == 0)
                    {
                        foreach (var assembly in GetAssembliesToCache())
                        {
                            _cachedAssemblies.TryAdd(assembly.FullName!, assembly);
                        }
                    }
                }
            }

            return _cachedAssemblies.Values;
        }
    }

    private static IEnumerable<Assembly> GetAssembliesToCache()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName is not null)
            .Where(a => !_assemblyPrefixesToSkip.Any(
                prefix => a.FullName!.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
    }
}