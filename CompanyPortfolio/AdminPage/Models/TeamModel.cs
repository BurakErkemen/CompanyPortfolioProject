using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    [FirestoreData]
    public class TeamModel
    {
        [FirestoreDocumentId]
        public string TeamId { get; set; } = default!;

        [FirestoreProperty]
        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "Ad ve soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        [Display(Name = "Ad Soyad")]
        [FirestoreProperty]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pozisyon bilgisi gereklidir.")]
        [StringLength(50, ErrorMessage = "Pozisyon en fazla 50 karakter olabilir.")]
        [Display(Name = "Görev / Pozisyon")]
        [FirestoreProperty]
        public string Role { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        [FirestoreProperty]
        public string? Email { get; set; } = string.Empty;

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [Display(Name = "Resim URL")]
        [FirestoreProperty]
        public string? ImageUrl { get; set; } = string.Empty;

        [Url(ErrorMessage = "Geçerli bir LinkedIn bağlantısı giriniz.")]
        [Display(Name = "LinkedIn Profili")]
        [FirestoreProperty]
        public string? LinkedInUrl { get; set; } = string.Empty;

        [Url(ErrorMessage = "Geçerli bir Twitter bağlantısı giriniz.")]
        [Display(Name = "Twitter Profili")]
        [FirestoreProperty]
        public string? TwitterUrl { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama / Biyografi")]
        [FirestoreProperty]
        public string? Description { get; set; } = string.Empty;

        [FirestoreProperty]
        public Timestamp CreatedAt { get; set; } = Timestamp.GetCurrentTimestamp();
    }
}
