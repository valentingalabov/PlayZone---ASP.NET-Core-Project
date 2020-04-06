namespace PlayZone.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PlayZone.Data.Models;

    public class ChanelConfiguration : IEntityTypeConfiguration<Chanel>
    {
        public void Configure(EntityTypeBuilder<Chanel> chanel)
        {
            chanel
                .HasOne(u => u.User)
                .WithOne(c => c.Chanel)
                .HasForeignKey<Chanel>(u => u.UserId);

            chanel
                .HasOne(i => i.Image)
                .WithOne(c => c.Chanel)
                .HasForeignKey<Image>(i => i.Id);
        }
    }
}
