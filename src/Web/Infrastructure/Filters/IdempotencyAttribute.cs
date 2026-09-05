using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoyaltyNet.Web.Infrastructure.Filters;

//[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
//public sealed class IdempotencyAttribute : TypeFilterAttribute
//{
//    /// <summary>
//    /// ctor
//    /// </summary>
//    /// <param name="tokenPrefix"></param>
//    public IdempotencyAttribute(string tokenPrefix) : base(typeof(IdempotencyActionFilter))
//    {
//        ArgumentNullException.ThrowIfNullOrWhiteSpace(tokenPrefix);

//        TokenPrefix = tokenPrefix;
//        Arguments = [tokenPrefix];
//    }

//    public string TokenPrefix { get; }

//    private sealed class IdempotencyActionFilter : IAsyncActionFilter
//    {
//        private const string IdempotencyTokenHeader = "Idempotency-Token";

//        private readonly string _tokenPrefix;
//        private readonly IIdempotencyTokenStore _tokenStore;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        /// <param name="tokenPrefix"></param>
//        /// <param name="tokenStore"></param>
//        public IdempotencyActionFilter(string tokenPrefix, IIdempotencyTokenStore tokenStore)
//        {
//            _tokenPrefix = tokenPrefix;
//            _tokenStore = tokenStore;
//        }

//        private async Task<bool> CheckIdempotencyTokenAsync(ActionExecutingContext context)
//        {
//            ArgumentNullException.ThrowIfNull(context);

//            var token = context.HttpContext.Request?.Headers?[IdempotencyTokenHeader].FirstOrDefault();

//            token = token?.Trim();

//            if (string.IsNullOrWhiteSpace(token))
//            {
//                context.Result = InvalidIdempotencyToken($"Заголовок запроса '{IdempotencyTokenHeader}' отсутствует или пустой", "idempotency_token_required");

//                return false;
//            }

//            if (token.Length > IdempotencyTokenConstraints.MaxTokenLength)
//            {
//                context.Result = InvalidIdempotencyToken($"Заголовок запроса '{IdempotencyTokenHeader}' превышает максимально допустимую длину", "idempotency_token_too_long");

//                return false;
//            }

//            if (!await _tokenStore.TryStoreAsync($"{_tokenPrefix}:{token}"))
//            {
//                context.Result = InvalidIdempotencyToken("Токен идемпотентности уже использован", "idempotency_token_already_used");

//                return false;
//            }

//            return true;
//        }

//        /// <inheritdoc/>
//        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {
//            if (await CheckIdempotencyTokenAsync(context))
//            {
//                await next();
//            }
//        }

//        private static ContentResult InvalidIdempotencyToken(string message, string code)
//        {
//            var result = new ProblemResponse(
//                status: StatusCodes.Status400BadRequest,
//                message: message,
//                code: code);

//            return new ContentResult()
//            {
//                StatusCode = StatusCodes.Status400BadRequest,
//                Content = ProblemResponseSerializer.Serialize(result),
//                ContentType = "application/json"
//            };
//        }
//    }
//}
