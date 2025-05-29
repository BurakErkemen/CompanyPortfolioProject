using Google.Cloud.Firestore;
using System;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly FirestoreDb _firestoreDb;

    public FirebaseService()
    {
        string path = @"C:\AdminPage\credential.json"; // Dosya yolunu kendi indirdiğinle değiştir
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        _firestoreDb = FirestoreDb.Create("portfoliowebservice-f981a"); // Firebase Project ID
    }

    /*public async Task AddAdminDataAsync(string ad, string email)
    {
        CollectionReference colRef = _firestoreDb.Collection("Admins");
        var data = new
        {
            Name = ad,
            Email = email,
            CreatedAt = DateTime.UtcNow
        };
        await colRef.AddAsync(data);
    }*/
}
