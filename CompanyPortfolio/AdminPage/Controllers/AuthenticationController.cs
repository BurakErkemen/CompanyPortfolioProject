using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Mvc;
using AdminPage.Models;

public class AuthenticationController : Controller
{
    private readonly FirebaseAuthClient _authClient;
    private const string FirebaseApiKey = "AIzaSyAhE5fpZyTuKTWpFgFnyyTjjGdLGWA6XaE";
    private const string AuthDomain = "portfoliowebservice-f981a.firebaseapp.com";

    public AuthenticationController()
    {
        var config = new FirebaseAuthConfig
        {
            ApiKey = FirebaseApiKey,
            AuthDomain = AuthDomain,
            Providers = new FirebaseAuthProvider[]
            {
                // Email ve şifre ile giriş sağlayıcısı
                new EmailProvider()
            }
        };

        _authClient = new FirebaseAuthClient(config);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (!ModelState.IsValid) return View(loginModel);
        try
        {
            var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(
                loginModel.Email,
                loginModel.Password);

            var token = await userCredential.User.GetIdTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Session.SetString("_UserToken", token);
                HttpContext.Session.SetString("_UserEmail", userCredential.User.Info.Email); // <== burası eklendi
                HttpContext.Session.SetString("_UserId", userCredential.User.Uid); // <== burası eklendi
                return RedirectToAction("Index", "Home");
            }


            ModelState.AddModelError("", "Token alınamadı.");
        }
        catch (FirebaseAuthException ex)
        {
            // Firebase özel hataları
            ModelState.AddModelError("", $"Hata: {ex.Reason.ToString()}");
        }
        catch (Exception ex)
        {
            // Genel hatalar
            ModelState.AddModelError("", $"Beklenmeyen hata: {ex.Message}");
        }

        return View(loginModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("_UserToken");
        _authClient.SignOut();
        return RedirectToAction("Login");
    }
}