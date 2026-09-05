using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using LoyaltyNet.Application.References.Mccs;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Web.Infrastructure;
using LoyaltyNet.Web.Infrastructure.Extensions;
using LoyaltyNet.Web.Infrastructure.Validation;
using LoyaltyNet.Web.References.Mccs.Models;
using LoyaltyNet.Web.References.Mccs.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LoyaltyNet.Web.References.Mccs;

[Tags("Справочники. MCC-коды")]
[Route("api/v1/references/mcc")]
public sealed class MccController(IMccQueryService queries, IMccCommandService commands) : ApiController
{
    private readonly IMccQueryService _queries = queries;
    private readonly IMccCommandService _commands = commands;

    /// <summary>
    /// Получение списка MCC
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Список MCC</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(200, "Успешное выполнение запроса", typeof(IList<MccModel>))]
    public async Task<IList<MccModel>> GetMccAsync(string? query = null)
    {
        var mccs = await _queries.GetMccAsync(query);

        return mccs.ToModelList();
    }

    /// <summary>
    /// Получение постраничного списка MCC
    /// </summary>
    /// <param name="pageIndex">Индекс страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="query"></param>
    /// <returns>Постраничный список MCC</returns>
    [HttpGet("pages")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(200, "Успешное выполнение запроса", typeof(PagedListResponse<MccModel>))]
    public async Task<PagedListResponse<MccModel>> GetPagedMccAsync(int pageIndex = 0, int pageSize = 1, string? query = null)
    {
        var pagedList = await _queries.GetPagedMccAsync(pageIndex, pageSize, query);

        return pagedList.ToResponse(e => e.ToModel());
    }

    /// <summary>
    /// Добавление MCC
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns></returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse(201, "Успешное выполнение запроса")]
    [SwaggerResponse(400, "Некорректные входные данные")]
    [SwaggerResponse(400, "Ресурс не может быть создан или изменен по причине каких-либо ограничений")]
    public async Task<IActionResult> CreateMccAsync(CreateMccRequest request)
    {
        var result = await _commands.CreateMccAsync(new MccCode(request.Code), request.Name);

        return result.IsSuccess ? Created() : Problem(result);
    }

    /// <summary>
    /// Обновление MCC
    /// </summary>
    /// <param name="code">MCC-код</param>
    /// <param name="request">Параметры запроса</param>
    /// <returns></returns>
    [HttpPatch("{code}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<IActionResult> UpdateMccAsync([MccCode] string code, UpdateMccRequest request)
    {
        var result = await _commands.UpdateMccAsync(new MccCode(code), request.Name);

        return result.IsSuccess ? NoContent() : Problem(result);
    }

    /// <summary>
    /// Удаление MCC
    /// </summary>
    /// <param name="code">MCC-код</param>
    /// <returns></returns>
    [HttpDelete("{code}")]
    [SwaggerResponse(204, "Успешное выполнение запроса")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<IActionResult> DeactivateMccAsync([MccCode] string code)
    {
        var result = await _commands.DeactivateMccAsync(new MccCode(code));

        return result.IsSuccess ? NoContent() : Problem(result);
    }
}