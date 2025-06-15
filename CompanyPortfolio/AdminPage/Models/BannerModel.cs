using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class BannerModel
    {
        [FirestoreDocumentId]
        public string BannerId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string Title { get; set; } = string.Empty;
            
        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ImageUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string LinkUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
