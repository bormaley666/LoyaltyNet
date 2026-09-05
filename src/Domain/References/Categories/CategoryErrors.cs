using FluentResults;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Categories;

public static class CategoryErrors
{
    public static Error NotFound(CategoryCode code) =>
        new Error($"Категория с кодом '{code}' не найдена")
            .WithMetadata("code", "not_found");

    public static Error MccAlreadyInCategory(MccCode code) =>
        new Error($"MCC '{code}' уже принадлежит категории")
            .WithMetadata("code", "mcc_already_in_category");

    public static Error MccNotInCategory(MccCode mccCode) =>
        new Error($"MCC '{mccCode}' не найден в категории")
            .WithMetadata("code", "mcc_not_in_category");

    public static Error AlreadyExists(CategoryCode code) =>
        new Error($"Категория с кодом '{code}' уже существует")
            .WithMetadata("code", "already_exists");

    public static Error DefaultAlreadyExists() =>
        new Error("Категория по умолчанию уже существует")
            .WithMetadata("code", "default_already_exists");
}
