using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarServer.Domain.Users;

namespace RentCarServer.Infrastructure.Configurations;
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.FirstName);
        builder.OwnsOne(p => p.LastName);
        builder.OwnsOne(p => p.FullName);
        builder.OwnsOne(p => p.Email);
        builder.OwnsOne(p => p.UserName);
        builder.OwnsOne(p => p.Password);
        builder.OwnsOne(P => P.ForgotPasswordId);
        builder.OwnsOne(P => P.ForgotPasswordDate);
        builder.OwnsOne(P => P.IsForgotPasswordCompleted);
    }
}
