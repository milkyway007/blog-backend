using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.V1
{
    public class BaseApiController : Controller
    {
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private IMediator _mediator;
    }
}
