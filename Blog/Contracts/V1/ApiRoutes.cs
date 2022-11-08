namespace Blog.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = $"{Root}/{Version}";

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Get = Base + "/posts/{postId:length(24)}";
            public const string Update = Base + "/posts/{postId:length(24)}";
            public const string Delete = Base + "/posts/{postId:length(24)}";
            public const string Create = Base + "/posts";
        }
    }
}
