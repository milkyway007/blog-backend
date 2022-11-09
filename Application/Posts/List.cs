using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<List<Post>> {}

        public class Handler : IRequestHandler<Query, List<Post>>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<List<Post>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _postService.GetPostsAsync();
            }
        }
    }
}
