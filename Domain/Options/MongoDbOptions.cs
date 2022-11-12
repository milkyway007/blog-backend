namespace Domain.Options
{
    public class MongoDbOptions
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string PostsCollectionName { get; set; }
        public string CommentsCollectionName { get; set; }

    }
}
