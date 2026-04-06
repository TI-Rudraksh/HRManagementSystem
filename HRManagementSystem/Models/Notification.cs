namespace HRManagementSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }      
        public string Color { get; set; }     
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
