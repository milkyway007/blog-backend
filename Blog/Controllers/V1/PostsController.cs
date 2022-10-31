using Blog.Contracts.V1.Requests;
using Blog.Contracts.V1.Responses;
using Blog.Domain;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(Contracts.V1.ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(Contracts.V1.ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(Contracts.V1.ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var post = new Post
            {
                Id = postId,
                Name = updatePostRequest.Name,
            };

            var isUpdated = _postService.UpdatePost(post);

            if (isUpdated)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var isDeleted = _postService.DeletePost(postId);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(Contracts.V1.ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };

            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();

            _postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" +
                Contracts.V1.ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new CreatePostResponse { Id = post.Id };

            return Created(locationUrl, response);
        }
    }
}
