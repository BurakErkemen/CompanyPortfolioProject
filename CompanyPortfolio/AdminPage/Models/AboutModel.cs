using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class AboutModel
    {
        [FirestoreDocumentId]
        public string AboutId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string Text { get; set; } = string.Empty;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
