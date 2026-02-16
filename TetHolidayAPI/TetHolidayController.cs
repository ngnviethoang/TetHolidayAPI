using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TetHolidayAPI;

[ApiController]
[Route("[controller]/[action]")]
public class TetHolidayController : ControllerBase
{
    private readonly TetHolidayDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TetHolidayController(TetHolidayDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            AccountNumber = dto.AccountNumber,
            Wish = dto.Wish,
            ImageName = dto.ImageName,
            LuckyMoney = dto.LuckyMoney
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost]
    [RequestSizeLimit(20 * 1024 * 1024)] // 20MB
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        const long maxFileSize = 20 * 1024 * 1024;

        if (file.Length > maxFileSize)
        {
            return BadRequest("File size exceeds 20MB limit.");
        }

        var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok(new
        {
            FileName = fileName
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users
            .OrderByDescending(x => x.Id)
            .ToListAsync();

        return Ok(users);
    }
}