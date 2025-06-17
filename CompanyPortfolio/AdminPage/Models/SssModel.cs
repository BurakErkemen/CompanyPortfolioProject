using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    [FirestoreData]
    public class SssModel
    {
        [FirestoreDocumentId]
        public string SssId { get; set; } = default!;

        [Required(ErrorMessage = "Kullanıcı ID gerekli")]
        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "Soru alanı zorunludur")]
        [FirestoreProperty]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cevap alanı zorunludur")]
        [FirestoreProperty]
        public string Answer { get; set; } = string.Empty;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;
    }
}
