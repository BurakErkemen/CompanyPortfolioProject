using Google.Cloud.Firestore;

namespace AdminPage.Service.GenericRepository
{
    public class FirebaseGenericRepository<T> : IFirebaseGenericRepository<T> where T : class
    {
        protected readonly FirestoreDb _firestore;

        public FirebaseGenericRepository(IConfiguration configuration)
        {
            // Ayarları config dosyasından al
            string projectId = configuration["Firebase:ProjectId"]!;
            string relativePath = configuration["Firebase:CredentialsPath"]!;

            // Uygulama çalışma diziniyle birleştir
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            Console.WriteLine("DEBUG path: " + fullPath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Credential dosyası bulunamadı", fullPath);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", fullPath);
            _firestore = FirestoreDb.Create(projectId);
        }


        public async Task<List<T>> GetAllAsync(string collectionName, string userId)
        {
            var query = _firestore.Collection(collectionName).WhereEqualTo("UserId", userId);
            var snapshot = await query.GetSnapshotAsync();

            return snapshot.Documents.Select(d => d.ConvertTo<T>()).ToList();
        }
        public async Task<T?> GetByUserIdAsync(string collectionName, string userId)
        {
            var query = _firestore.Collection(collectionName)
                                  .WhereEqualTo("UserId", userId)
                                  .Limit(1); // Tek kullanıcıya ait tek kayıt varsayımı

            var snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return null;

            return snapshot.Documents[0].ConvertTo<T>();
        }


        public async Task<T?> GetByIdAsync(string collectionName, string id, string userId)
        {
            var docRef = _firestore.Collection(collectionName).Document(id);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            var entity = snapshot.ConvertTo<T>();

            // userId kontrolü: sadece kendine ait belgeye erişebilsin
            var userIdField = snapshot.GetValue<string>("UserId");

            return userIdField == userId ? entity : null;
        }

        public async Task AddAsync(string collectionName, T entity, string userId)
        {
            // Gerekirse burada entity.UserId'yi set edebilirsin (reflection ile veya override ederek)
            await _firestore.Collection(collectionName).AddAsync(entity);
        }

        public async Task UpdateAsync(string collectionName, string id, T entity, string userId)
        {
            var docRef = _firestore.Collection(collectionName).Document(id);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists || snapshot.GetValue<string>("UserId") != userId)
                return;

            await docRef.SetAsync(entity);
        }

        public async Task DeleteAsync(string collectionName, string id, string userId)
        {
            var docRef = _firestore.Collection(collectionName).Document(id);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists || snapshot.GetValue<string>("UserId") != userId)
                return;

            await docRef.DeleteAsync();
        }
    }
}
