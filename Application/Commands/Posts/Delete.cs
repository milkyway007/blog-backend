using Domain.Entities;
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
            private readonly IService<Post> _postService;

            public Handler(IService<Post> postService)
            {
                _postService = postService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _postService.DeleteAsync(request.Id, cancellationToken);
            }
        }
    }
}
