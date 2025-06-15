using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace AdminPage.Models
{
    [FirestoreData]
    public class AboutModel
    {
        [FirestoreDocumentId]
        public string AboutId { get; set; } = default!;

        [Required(ErrorMessage = "Kullanıcı ID gerekli")]
        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "Açıklama alanı zorunludur")]
        [MinLength(5, ErrorMessage = "Açıklama en az 5 karakter olmalıdır")]
        [FirestoreProperty]
        public string Text { get; set; } = string.Empty;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
