using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class BlogModel
    {
        [FirestoreDocumentId]
        public string BlogId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string Title { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Content { get; set; } = string.Empty;

        [FirestoreProperty]
        public List<string> Tags { get; set; } = [];

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();

    }
}
