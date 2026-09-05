using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using LoyaltyNet.Application.References.Categories;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Web.Infrastructure;
using LoyaltyNet.Web.Infrastructure.Validation;
using LoyaltyNet.Web.References.Categories.Models;
using LoyaltyNet.Web.References.Categories.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LoyaltyNet.Web.References.Categories;

[Tags("Справочники. MCC-категории")]
[ApiController]
[Route("api/v1/references/categories")]
public sealed class CategoryController(ICategoryQueryService queries, ICategoryCommandService commands) : ApiController
{
    private readonly ICategoryQueryService _queries = queries;
    private readonly ICategoryCommandService _commands = commands;

    /// <summary>
    /// Получение списка категорий
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(200, "Успешное выполнение запроса", typeof(IList<CategoryModel>))]
    public async Task<IList<CategoryModel>> GetCategoriesAsync()
    {
        var categories = await _queries.GetCategoriesAsync();

        return categories.ToModelList();
    }

    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns></returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(400, "Некорректные входные данные")]
    [SwaggerResponse(400, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var result = await _commands.CreateCategoryAsync(new CategoryCode(request.Code), request.Name, request.Description);

        return result.IsSuccess ? Created() : Problem(result);
    }

    /// <summary>
    /// Обновление категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="request">Параметры запроса</param>
    /// <returns></returns>
    [HttpPatch("{code:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(400, "Некорректные входные данные")]
    [SwaggerResponse(404, "Ресурс не найден")]
    [SwaggerResponse(400, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> UpdateCategoryAsync([CategoryCode] int code, UpdateCategoryRequest request)
    {
        var result = await _commands.UpdateCategoryAsync(new CategoryCode(code), request.Name, request.Description);

        return result.IsSuccess ? NoContent() : Problem(result);
    }

    /// <summary>
    /// Удаление категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <returns></returns>
    [HttpDelete("{code:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(404, "Ресурс не найден")]
    [SwaggerResponse(409, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> DeactivateCategoryAsync([CategoryCode] int code)
    {
        var result = await _commands.DeactivateCategoryAsync(new CategoryCode(code));

        return result.IsSuccess ? NoContent() : Problem(result);
    }

    /// <summary>
    /// Добавление MCC-кода в список категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="mccCode">Код MCC</param>
    /// <returns></returns>
    [HttpPut("{code:int}/mcc/{mccCode}")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(404, "Ресурс не найден")]
    [SwaggerResponse(400, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> AttachCategoryAsync([CategoryCode] int code, [MccCode] string mccCode)
    {
        var result = await _commands.AttachCategoryAsync(new CategoryCode(code), new MccCode(mccCode));

        return result.IsSuccess ? NoContent() : Problem(result);
    }

    /// <summary>
    /// Удаление MCC-кода из списка категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="mccCode">Код MCC</param>
    /// <returns></returns>
    [HttpDelete("{code:int}/mcc/{mccCode}")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(404, "Ресурс не найден")]
    [SwaggerResponse(400, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> DetachCategoryAsync([CategoryCode] int code, [MccCode] string mccCode)
    {
        var result = await _commands.DetachCategoryAsync(new CategoryCode(code), new MccCode(mccCode));

        return result.IsSuccess ? NoContent() : Problem(result);
    }
}
