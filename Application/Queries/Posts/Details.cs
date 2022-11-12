using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Queries.Posts
{
    public class Details
    {
        public class Query : IRequest<Post>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Post>
        {
            private readonly IService<Post> _postService;

            public Handler(IService<Post> postService)
            {
                _postService = postService;
            }

            public async Task<Post> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _postService.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
