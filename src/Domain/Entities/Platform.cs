namespace MyGameStat.Domain.Entities
{
    public class Platform
    public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string Manufacturer { get; set; }
        
        // Navigation property
        public ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
    
}