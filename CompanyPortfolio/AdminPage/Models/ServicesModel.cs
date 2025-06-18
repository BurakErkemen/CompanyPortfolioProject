using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class ServicesModel
    {
        [FirestoreDocumentId]
        public string ServicesId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string ServiceName { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ServiceDescription { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ServiceImage { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ServiceLink { get; set; } = string.Empty;

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}   
