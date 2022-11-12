using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Queries.Comments
{
    public class Details
    {
        public class Query : IRequest<Comment>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Comment>
        {
            private readonly IService<Comment> _commentService;

            public Handler(IService<Comment> commentService)
            {
                _commentService = commentService;
            }

            public async Task<Comment> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _commentService.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
