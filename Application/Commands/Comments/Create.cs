using Domain.Entities;
using MediatR;
using Persistence;
using Persistence.Interfaces;

namespace Application.Commands.Comments
{
    public class Create
    {
        public class Command : IRequest
        {
            public Comment Comment { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IService<Comment> _commentService;

            public Handler(IService<Comment> commentService)
            {
                _commentService = commentService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _commentService.CreateAsync(request.Comment, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
