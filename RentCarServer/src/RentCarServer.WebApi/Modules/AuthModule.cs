﻿using RentCarServer.Application.Auth;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebApi.Modules;

public static class AuthModule
{
    public static void MapAuth(this IEndpointRouteBuilder builder)
    {
        var app = builder.MapGroup("/auth");

        app.MapPost("/login",
            async (LoginCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful
                    ? Results.Ok(res)
                    : Results.InternalServerError(res);
            })
            .Produces<Result<string>>()
            .RequireRateLimiting("login-fixed"); ;

        app.MapPost("/forgot-password/{email}",
            async (string email, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(new ForgotPasswordCommand(email), cancellationToken);
                return res.IsSuccessful
                    ? Results.Ok(res)
                    : Results.InternalServerError(res);
            })
            .Produces<Result<string>>()
            .RequireRateLimiting("forgot-password-fixed");

        app.MapPost("/reset-password",
            async (ResetPasswordCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful
                    ? Results.Ok(res)
                    : Results.InternalServerError(res);
            })
            .Produces<Result<string>>()
            .RequireRateLimiting("reset-password-fixed");
    }
}
