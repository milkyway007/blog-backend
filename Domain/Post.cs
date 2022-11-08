using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Blog.Domain
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
    }
}
