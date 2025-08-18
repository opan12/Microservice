
using Microservice.Shared.DTO;
using Microservice.user.api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MongoDbContext _mongoContext;
    private readonly IConfiguration _configuration;
    private readonly IDatabase _redisDb;


    public UserController(MongoDbContext mongoContext, IConfiguration configuration)
    {
        _mongoContext = mongoContext;
        _configuration = configuration;
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _redisDb = redis.GetDatabase();

    }
    [HttpPost("logindeneme")]
    public async Task<IActionResult> Deneme([FromBody] LoginModel model)
    {
        var user = await _mongoContext.Users
            .Find(u => u.Username == model.Username && u.Password == model.Password)
            .FirstOrDefaultAsync();

        if (user == null)
            return Unauthorized("Kullanıcı adı veya şifre hatalı.");

        var userSession = new UserSessionDto
        {
            UserId = user.Id.ToString(),
            Username = user.Username,
            MusteriNo = user.MusteriNo 
        };

        var sessionId = Guid.NewGuid().ToString();

        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var db = redis.GetDatabase();

        db.StringSet(sessionId, JsonConvert.SerializeObject(userSession), TimeSpan.FromMinutes(30));

        return Ok(new { SessionId = sessionId });
    }
    
    public class MusteriBasvuruRequest
    {
        public string BasvuruTipi { get; set; }
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
        {
            RedirectUri = "/auth/profile"
        }, "Google");
    }
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var userClaims = User.Claims.ToList();

        var userSession = new UserSessionDto
        {
            UserId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
            Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
            Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
        };

        var sessionId = Guid.NewGuid().ToString();

        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var db = redis.GetDatabase();

        db.StringSet(sessionId, JsonConvert.SerializeObject(userSession), TimeSpan.FromMinutes(30));

        return Ok(new { SessionId = sessionId, User = userSession });
    }


    [HttpGet("kullanicilar")]
public IActionResult TumKullanici()
{
    var users = _mongoContext.Users.Find(_ => true).ToList();
    return Ok(users);
}
    //private async Task<string> GenerateJwtToken(User user)
    //{
    //    var claims = new[]
    //    {
    //    new Claim(JwtRegisteredClaimNames.Sub, user.MusteriNo),
    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //      new Claim(ClaimTypes.NameIdentifier, user.Id),
    //};

    //    // 32 karakterden kısa olmayacak
    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey1234567890SuperSecretKey1234567890"));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: "GorevNet",
    //        audience: "GorevNetUsers",
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddMinutes(30),
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}







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
