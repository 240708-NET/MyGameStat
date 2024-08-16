namespace MyGameStat.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        
        // Navigation property
        public ICollection<Collection> Collections { get; set; }
    }
}