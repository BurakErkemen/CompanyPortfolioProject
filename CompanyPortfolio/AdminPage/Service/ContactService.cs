using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class ContactService : FirebaseGenericRepository<ContactModel>
    {
        public ContactService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
