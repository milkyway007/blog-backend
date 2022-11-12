using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Commands.Comments
{
    public class Edit
    {
        public class Query : IRequest<bool>
        {
            public Comment Comment { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly IService<Comment> _commentService;

            public Handler(IService<Comment> commentService)
            {
                _commentService = commentService;
            }

            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _commentService.UpdateAsync(request.Comment, cancellationToken);
            }
        }
    }
}
