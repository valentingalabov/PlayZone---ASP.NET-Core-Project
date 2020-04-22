namespace PlayZone.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlayZone.Data.Models;

    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> channel)
        {
            channel
                .HasOne(u => u.User)
                .WithOne(c => c.Channel)
                .HasForeignKey<Channel>(u => u.UserId);

            channel
                .HasOne(i => i.Image)
                .WithOne(c => c.Channel)
                .HasForeignKey<Image>(i => i.Id);
        }
    }
}
