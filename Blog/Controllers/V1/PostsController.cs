using Application.Commands.Posts;
using Application.Posts;
using Blog.Contracts.V1.Requests;
using Blog.Contracts.V1.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.V1
{
    public class PostsController : BaseApiController
    {
        [HttpGet(Contracts.V1.ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new List.Query(), cancellationToken));
        }

        [HttpGet(Contracts.V1.ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get(string postId, CancellationToken cancellationToken)
        {
            var post = await Mediator.Send(new Details.Query { Id = postId}, cancellationToken);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPatch(Contracts.V1.ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update(
            string postId,
            UpdatePostRequest updatePostRequest,
            CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Id = postId,
                Title = updatePostRequest.Title,
                Message = updatePostRequest.Message,
                Modified = DateTime.Now,
            };

            var isUpdated = await Mediator.Send(new Edit.Query { Post = post }, cancellationToken);
            if (isUpdated)
            {
                return Ok(post);
            }

            return NotFound();
        }

        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete(string postId, CancellationToken cancellationToken)
        {
            var isDeleted = await Mediator.Send(new Delete.Command { Id = postId }, cancellationToken);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(Contracts.V1.ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create(CreatePostRequest postRequest, CancellationToken cancellationToken)
        {
            var post = new Post
            { 
                Title = postRequest.Title,
                Message = postRequest.Message,
                Created = DateTime.Now,
                Modified = DateTime.Now,
            };

            await Mediator.Send(new Create.Command { Post = post}, cancellationToken);
            var response = new CreatePostResponse { Id = post.Id };

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" +
                Contracts.V1.ApiRoutes.Posts.Get.Replace("{postId}", post.Id);            

            return Created(locationUrl, response);
        }
    }
}
