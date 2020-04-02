namespace PlayZone.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlayZone.Data.Models;

    public class FavoriteVideoConfiguration : IEntityTypeConfiguration<FavoriteVideo>
    {
        public void Configure(EntityTypeBuilder<FavoriteVideo> builder)
        {
            builder
                .HasKey(fv => new { fv.UserId, fv.VideoId });
        }
    }
}
