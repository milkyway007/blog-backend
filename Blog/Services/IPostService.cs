using Blog.Domain;

namespace Blog.Services
{
    public interface IPostService
    {
        Task CreateAsync(Post post);

        Task<List<Post>> GetPostsAsync();

        Task<Post> GetPostByIdAsync(string postId);

        Task<bool> UpdatePostAsync(Post postToUpdate);

        Task<bool> DeletePostAsync(string postId);
    }
}
