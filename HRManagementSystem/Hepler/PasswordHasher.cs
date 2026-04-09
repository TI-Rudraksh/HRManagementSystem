using System.Text;

namespace HRManagementSystem.Hepler
{
    public static class PasswordHelper
    {
        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                return null;

            byte[] bytes = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
