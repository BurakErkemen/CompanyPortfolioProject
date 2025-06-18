using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class BannerService : FirebaseGenericRepository<BannerModel>
    {
        public BannerService(IConfiguration configuration) : base(configuration)
        {
        }

    }
}
