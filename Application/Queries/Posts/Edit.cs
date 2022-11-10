using Domain.Entities;
using MediatR;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Posts
{
    public class Edit
    {
        public class Query : IRequest<Post>
        {
            public Post Post { get; set; }
        }

        public class Handler : IRequestHandler<Query, Post>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<Post> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _postService.UpdatePostAsync(request.Post, cancellationToken);
            }
        }
    }
}
