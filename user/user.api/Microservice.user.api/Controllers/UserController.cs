using Microservice.user.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MongoDbContext _mongoContext;
    public UserController(MongoDbContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    [HttpPost("logindeneme")]
    public async Task<IActionResult> Deneme([FromBody] LoginModel model)
    {
        var user = await _mongoContext.Users
            .Find(u => u.Username == model.Username && u.Password == model.Password)
            .FirstOrDefaultAsync();

        if (user == null)
            return Unauthorized("Kullanıcı adı veya şifre hatalı.");

        return Ok();

    }[HttpGet("login")]
    public IActionResult Login()
    {
        // Google giriş ekranına yönlendir
        return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
        {
            RedirectUri = "/auth/profile"
        }, "Google");
    }

   [HttpGet("kullanicilar")]
public IActionResult TumKullanici()
{
    var users = _mongoContext.Users.Find(_ => true).ToList();
    return Ok(users);
}



    [HttpPost("kayitolustur")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User
        {
            Username = model.Username,
            Ad = model.Ad,
            Soyad = model.Soyad,
            TCKimlikNO = model.TCKimlikNO,
            MusteriNo = "123456",
            Email = model.Email,
            DogumTarihi = model.DogumTarihi,
            Role = "User",
            Password = model.Password,
        };

        await _mongoContext.Users.InsertOneAsync(user);

        return Ok(new { Message = "User created successfully." });
    }

    
}
