using Blog.Domain;
using Blog.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(IOptions<MongoDbOptions> mongoDbOptions)
        {
            MongoClient client = new MongoClient(mongoDbOptions.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbOptions.Value.DatabaseName);

            _posts = database.GetCollection<Post>(mongoDbOptions.Value.CollectionName);
        }

        public async Task CreateAsync(Post post)
        {
            await _posts.InsertOneAsync(post);
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postId);
            var deleted = await _posts.FindOneAndDeleteAsync(filter);

            return deleted != null;
        }

        public async Task<Post> GetPostByIdAsync(string postId)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postId);

            return await _posts.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _posts.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postToUpdate.Id);
            UpdateDefinition<Post> update = Builders<Post>.Update.AddToSet("name", postToUpdate.Title);

            var updated = await _posts.FindOneAndUpdateAsync(filter, update);

            return updated != null;
        }
    }
}
