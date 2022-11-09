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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new List.Query()));
        }

        [HttpGet(Contracts.V1.ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] string postId)
        {
            var post = await Mediator.Send(new Details.Query { Id = postId});
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(Contracts.V1.ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] string postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var post = new Post
            {
                Id = postId,
                Title = updatePostRequest.Name,
            };

            var isUpdated = await _postService.UpdatePostAsync(post);
            if (isUpdated)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string postId)
        {
            var isDeleted = await _postService.DeletePostAsync(postId);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(Contracts.V1.ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Title = postRequest.Name };

            await _postService.CreateAsync(post);
            var response = new CreatePostResponse { Id = post.Id };

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" +
                Contracts.V1.ApiRoutes.Posts.Get.Replace("{postId}", post.Id);            

            return Created(locationUrl, response);
        }
    }
}
