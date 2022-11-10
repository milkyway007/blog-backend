using Domain.Entities;

namespace Persistence.Interfaces
{
    public interface IPostService
    {
        Task CreateAsync(Post post, CancellationToken cancellationToken);

        Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken);

        Task<Post> GetPostByIdAsync(string postId, CancellationToken cancellationToken);

        Task<Post> UpdatePostAsync(Post postToUpdate, CancellationToken cancellationToken);

        Task<bool> DeletePostAsync(string postId, CancellationToken cancellationToken);
    }
}
