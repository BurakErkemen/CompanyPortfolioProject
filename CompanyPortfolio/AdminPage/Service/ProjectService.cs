using AdminPage.Models;
using AdminPage.Service.GenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Service
{
    public class ProjectService : FirebaseGenericRepository<ProjectsModel>
    {
        public ProjectService(IConfiguration configuration) : base(configuration)
        {
        }
        

    }
}
