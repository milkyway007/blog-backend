using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Commands.Comments
{
    public class Delete
    {
        public class Command : IRequest<bool>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IService<Comment> _commentService;

            public Handler(IService<Comment> commentService)
            {
                _commentService = commentService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _commentService.DeleteAsync(request.Id, cancellationToken);
            }
        }
    }
}
