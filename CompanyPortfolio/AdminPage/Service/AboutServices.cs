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
        public Task<AboutModel?> GetLatestAsync(string userId)
        {
            return GetAllAsync("about", userId).ContinueWith(t => t.Result.FirstOrDefault());
        }

    }
}
