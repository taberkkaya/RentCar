using GenericRepository;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;

namespace RentCarServer.WebApi;

public static class ExtensionMethods
{
    public static async Task CreateFirstUser(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (!(await userRepository.AnyAsync(p => p.UserName.Value == "admin")))
        {
            FirstName firstName = new("Ataberk");
            LastName lastName = new("Kaya");
            Email email = new("ataberk@ataberkkaya.com");
            UserName userName = new("admin");
            Password password = new("1");

            var user = new User(firstName,lastName,email,userName,password);

            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
