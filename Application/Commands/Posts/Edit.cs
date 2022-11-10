using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Posts
{
    public class Edit
    {
        public class Query : IRequest<bool>
        {
            public Post Post { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly IPostService _postService;

            public Handler(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _postService.UpdatePostAsync(request.Post, cancellationToken);
            }
        }
    }
}
