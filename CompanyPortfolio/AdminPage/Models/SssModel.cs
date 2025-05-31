using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class SssModel
    {
        [FirestoreDocumentId]
        public string SssId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string Question { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Answer { get; set; } = string.Empty;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;
    }
}
