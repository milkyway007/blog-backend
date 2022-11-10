using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Posts
{
    public class Details
    {
        public class Query : IRequest<Post>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Post>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<Post> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _postService.GetPostByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
