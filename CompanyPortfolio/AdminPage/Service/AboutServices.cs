using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class AboutServices : FirebaseGenericRepository<AboutModel>
    {
        public AboutServices(IConfiguration config)
            : base(config)
        {
        }

    }
}
