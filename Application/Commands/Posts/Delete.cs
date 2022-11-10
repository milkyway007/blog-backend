using MediatR;
using Persistence.Interfaces;

namespace Application.Commands.Posts
{
    public class Delete
    {
        public class Command : IRequest<bool>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _postService.DeletePostAsync(request.Id, cancellationToken);
            }
        }
    }
}
