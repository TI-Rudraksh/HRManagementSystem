using HRManagementSystem.Data;
using HRManagementSystem.Data;
using HRManagementSystem.Hepler;
using HRManagementSystem.Models;
using HRManagementSystem.ViewModel;
using System.IdentityModel.Tokens.Jwt;

public class AuthService
{
    private readonly DB _context;

    public AuthService(DB context)
    {
        _context = context;
    }

    // REGISTER
    public string Register(RegisterViewModel model)
    {
        if (_context.Users.Any(x => x.Email == model.Email))
            return "User already exists";

        var userRole = _context.Roles.FirstOrDefault(r => r.RoleName == "User");

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = PasswordHelper.Encrypt(model.Password), // 🔥 CHANGED
            RoleId = userRole.RoleId
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return "Registered Successfully";
    }

    // LOGIN
    public string Login(LoginViewModel model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

        if (user == null)
            return "Invalid Email";

        var decrypted = PasswordHelper.Decrypt(user.PasswordHash);

        if (decrypted != model.Password)
            return "Invalid Password";

        var role = _context.Roles.Find(user.RoleId)?.RoleName;

        return JwtHelper.GenerateToken(user, role);
    }
}