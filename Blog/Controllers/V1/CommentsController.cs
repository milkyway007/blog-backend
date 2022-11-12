using Application.Commands.Comments;
using Application.Queries.Comments;
using Blog.Contracts.V1.Requests;
using Blog.Contracts.V1.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.V1
{
    public class CommentsController : BaseApiController
    {
        [HttpGet(Contracts.V1.ApiRoutes.Comments.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new List.Query(), cancellationToken));
        }

        [HttpGet(Contracts.V1.ApiRoutes.Comments.Get)]
        public async Task<IActionResult> Get(string commentId, CancellationToken cancellationToken)
        {
            var comment = await Mediator.Send(new Details.Query { Id = commentId }, cancellationToken);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPatch(Contracts.V1.ApiRoutes.Comments.Update)]
        public async Task<IActionResult> Update(
            string commentId,
            UpdateCommentRequest updateCommentRequest,
            CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Id = commentId,
                Message = updateCommentRequest.Message,
                Modified = DateTime.Now,
            };

            var isUpdated = await Mediator.Send(new Edit.Query { Comment = comment }, cancellationToken);
            if (isUpdated)
            {
                return Ok(comment);
            }

            return NotFound();
        }

        [HttpDelete(Contracts.V1.ApiRoutes.Comments.Delete)]
        public async Task<IActionResult> Delete(string commentId, CancellationToken cancellationToken)
        {
            var isDeleted = await Mediator.Send(new Delete.Command { Id = commentId }, cancellationToken);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(Contracts.V1.ApiRoutes.Comments.Create)]
        public async Task<IActionResult> Create(CreateCommentRequest commentRequest, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Message = commentRequest.Message,
                Created = DateTime.Now,
                Modified = DateTime.Now,
            };

            await Mediator.Send(new Create.Command { Comment = comment }, cancellationToken);
            var response = new CreatePostResponse { Id = comment.Id };

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" +
                Contracts.V1.ApiRoutes.Posts.Get.Replace("{commentId}", comment.Id);            

            return Created(locationUrl, response);
        }
    }
}
