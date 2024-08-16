namespace MyGameStat.Domain.Entities
{
    public class VideoGame
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        
        // Navigation property
        public ICollection<Collection> Collections { get; set; }
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
    }
}