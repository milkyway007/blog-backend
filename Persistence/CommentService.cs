using AutoMapper;
using Domain.Entities;
using Domain.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Persistence
{
    public class CommentService : IService<Comment>
    {
        private readonly IMongoCollection<Comment> _comments;
        private readonly IMapper _mapper;

        public CommentService(IOptions<MongoDbOptions> mongoDbOptions, IMapper mapper)
        {
            MongoClient client = new MongoClient(mongoDbOptions.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbOptions.Value.DatabaseName);

            _comments = database.GetCollection<Comment>(mongoDbOptions.Value.CommentsCollectionName);

            _mapper = mapper;
        }

        public async Task CreateAsync(Comment comment, CancellationToken cancellationToken)
        {
            await _comments.InsertOneAsync(comment, null, cancellationToken);
        }

        public async Task<bool> DeleteAsync(string commentId, CancellationToken cancellationToken)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("Id", commentId);
            var deleted = await _comments.FindOneAndDeleteAsync(filter, null, cancellationToken);

            return deleted != null;
        }

        public async Task<Comment> GetByIdAsync(string commentId, CancellationToken cancellationToken)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("Id", commentId);

            return await _comments.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Comment>> GetAsync(CancellationToken cancellationToken)
        {
            return await _comments.Find(new BsonDocument()).ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Comment commentToUpdate, CancellationToken cancellationToken)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq("Id", commentToUpdate.Id);
            UpdateDefinition<Comment> update = Builders<Comment>.Update
                .Set(p => p.Message, commentToUpdate.Message)
                .Set(p => p.Modified, commentToUpdate.Modified);

            var found = await _comments.FindOneAndUpdateAsync(filter, update, null, cancellationToken);
            if (found == null)
                return false;

            _mapper.Map(found, commentToUpdate);

            return true;
        }
    }
}
