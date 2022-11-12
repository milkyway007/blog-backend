using AutoMapper;
using Domain.Entities;
using Domain.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Persistence
{
    public class PostService : IService<Post>
    {
        private readonly IMongoCollection<Post> _posts;
        private readonly IMapper _mapper;

        public PostService(IOptions<MongoDbOptions> mongoDbOptions, IMapper mapper)
        {
            MongoClient client = new MongoClient(mongoDbOptions.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbOptions.Value.DatabaseName);

            _posts = database.GetCollection<Post>(mongoDbOptions.Value.PostsCollectionName);

            _mapper = mapper;
        }

        public async Task CreateAsync(Post post, CancellationToken cancellationToken)
        {
            await _posts.InsertOneAsync(post, null, cancellationToken);
        }

        public async Task<bool> DeleteAsync(string postId, CancellationToken cancellationToken)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postId);
            var deleted = await _posts.FindOneAndDeleteAsync(filter, null, cancellationToken);

            return deleted != null;
        }

        public async Task<Post> GetByIdAsync(string postId, CancellationToken cancellationToken)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postId);

            return await _posts.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Post>> GetAsync(CancellationToken cancellationToken)
        {
            return await _posts.Find(new BsonDocument()).ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Post postToUpdate, CancellationToken cancellationToken)
        {
            FilterDefinition<Post> filter = Builders<Post>.Filter.Eq("Id", postToUpdate.Id);
            UpdateDefinition<Post> update = Builders<Post>.Update
                .Set(p => p.Title, postToUpdate.Title)
                .Set(p => p.Message, postToUpdate.Message)
                .Set(p => p.Modified, postToUpdate.Modified);

            var found = await _posts.FindOneAndUpdateAsync(filter, update, null, cancellationToken);
            if (found == null)
                return false;

            _mapper.Map(found, postToUpdate);

            return true;
        }
    }
}
