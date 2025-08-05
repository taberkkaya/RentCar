using FluentValidation;
using GenericRepository;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Auth;
public sealed record ResetPasswordCommand(
    Guid ForgotPasswordId, 
    string NewPassword) : IRequest<Result<string>>;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Geçerli bir yeni şifre giriniz.");
    }
}

internal sealed class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FirstOrDefaultAsync(p => 
            p.ForgotPasswordId != null 
            && p.ForgotPasswordId.Value == request.ForgotPasswordId
            && p.IsForgotPasswordCompleted.Value == false 
            );

        if (user is null)
            return Result<string>.Failure("Kullanıcı bulunamadı!");

        var fpDate = user.ForgotPasswordDate!.Value.AddDays(1);
        var now = DateTimeOffset.Now;
        
        if (fpDate < now)
            return Result<string>.Failure("Şifre sıfırlama anahtarı geçersiz!");

        Password password = new(request.NewPassword);
        user.SetPassword(password);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Şifre başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.";    
    }
}
