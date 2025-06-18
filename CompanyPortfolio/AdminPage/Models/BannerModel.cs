using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; } = default!;

        [FirestoreProperty]
        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [StringLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir.")]
        public string Description { get; set; } = default!;

        [FirestoreProperty]
        [Required(ErrorMessage = "Görsel URL zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir görsel bağlantısı giriniz.")]
        public string ImageUrl { get; set; } = default!;

        [FirestoreProperty]
        [Url(ErrorMessage = "Geçerli bir yönlendirme bağlantısı giriniz.")]
        public string? LinkUrl { get; set; } = string.Empty;

        [FirestoreProperty]
        public bool IsActive { get; set; } = true;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
