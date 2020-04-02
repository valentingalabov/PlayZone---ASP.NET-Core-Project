namespace PlayZone.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlayZone.Data.Models;

    public class VideoHistoryConfiguration : IEntityTypeConfiguration<VideoHistory>
    {
        public void Configure(EntityTypeBuilder<VideoHistory> builder)
        {
            builder
                .HasKey(vh => new { vh.UserId, vh.VideoId });
        }
    }
}
