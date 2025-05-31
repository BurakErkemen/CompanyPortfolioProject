using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class ProjectsModel
    {
        [FirestoreDocumentId]
        public string AboutId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string Title{ get; set; } = string.Empty;

        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ImageUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Link { get; set; } = string.Empty;

        [FirestoreProperty]
        public List<string> Tags { get; set; } = [];

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
