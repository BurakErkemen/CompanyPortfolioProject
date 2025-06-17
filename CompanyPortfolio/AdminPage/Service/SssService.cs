using AdminPage.Models;
using AdminPage.Service.GenericRepository;

namespace AdminPage.Service
{
    public class SssService : FirebaseGenericRepository<SssModel>
    {
        public SssService(IConfiguration configuration) : base(configuration)
        {
        }

        // Is Active SSS'leri getirir
        public async Task<List<SssModel>> GetActiveSssAsync(string userId)
        {
            var query = _firestore.Collection("SSS").WhereEqualTo("IsActive", true);
            return await GetAllAsync("SSS",userId);
        }

    }
}
