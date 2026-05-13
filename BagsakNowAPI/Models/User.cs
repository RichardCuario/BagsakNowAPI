namespace BagsakNowAPI.Models
{
    public class Member
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public string Role { get; set; } = "Member";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}