using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class BlogService : FirebaseGenericRepository<BlogModel>
    {
        public BlogService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
