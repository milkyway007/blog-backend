using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Commands.Posts
{
    public class Create
    {
        public class Command : IRequest
        {
            public Post Post { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _postService.CreateAsync(request.Post, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
