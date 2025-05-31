using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class SettingsModel
    {
        [FirestoreDocumentId]
        public string SettingsId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [FirestoreProperty]
        public string SiteName { get; set; } = string.Empty;

        [FirestoreProperty]
        public string SiteDescription { get; set; } = string.Empty;

        [FirestoreProperty]
        public string SiteKeywords { get; set; } = string.Empty;

        [FirestoreProperty]
        public string SiteLogoUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string SiteFaviconUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ContactEmail { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ContactPhone { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ContactAddress { get; set; } = string.Empty;

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
