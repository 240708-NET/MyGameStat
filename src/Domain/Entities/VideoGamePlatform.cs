namespace MyGameStat.Domain.Entities
{
    public class VideoGamePlatform
    {
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        
        // Navigation properties
        public VideoGame VideoGame { get; set; }
        public Platform Platform { get; set; }
    }
}