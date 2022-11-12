using Blog.Interfaces.Installers;
using Domain.Entities;
using Domain.Options;
using Persistence;
using Persistence.Interfaces;

namespace Blog.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection("MongoDB"));

            services.AddSingleton<IService<Post>, PostService>();
            services.AddSingleton<IService<Comment>, CommentService>();
        }
    }
}
