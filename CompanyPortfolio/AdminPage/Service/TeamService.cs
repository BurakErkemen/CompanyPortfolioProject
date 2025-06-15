using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class TeamService : FirebaseGenericRepository<TeamModel>
    {
        public TeamService(IConfiguration configuration) : base(configuration)
        {
        }

    }
}
