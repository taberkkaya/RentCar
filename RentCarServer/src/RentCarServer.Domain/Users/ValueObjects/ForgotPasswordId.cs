using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Domain.Users.ValueObjects;

public sealed record ForgotPasswordId(Guid Value)
{
    public static implicit operator Guid(ForgotPasswordId id) => id.Value;

    public static implicit operator string(ForgotPasswordId id) => id.Value.ToString();
}
