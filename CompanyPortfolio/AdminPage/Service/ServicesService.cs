using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class ServicesService :FirebaseGenericRepository<ServicesModel>
    {
        public ServicesService(IConfiguration config) : base(config)
        {
        }

    }
}
