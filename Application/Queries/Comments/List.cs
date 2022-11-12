using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Queries.Comments
{
    public class List
    {
        public class Query : IRequest<List<Comment>> {}

        public class Handler : IRequestHandler<Query, List<Comment>>
        {
            private readonly IService<Comment> _commentService;

            public Handler(IService<Comment> commentService)
            {
                _commentService = commentService;
            }

            public async Task<List<Comment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _commentService.GetAsync(cancellationToken);
            }
        }
    }
}
