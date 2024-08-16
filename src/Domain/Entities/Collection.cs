namespace MyGameStat.Domain.Entities
{
    public class Collection
    {
        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Status { get; set; } // E.g., Owned, Wishlist, Playing, Completed
        
        // Navigation properties
        public User User { get; set; }
        public VideoGame VideoGame { get; set; }
    }
}