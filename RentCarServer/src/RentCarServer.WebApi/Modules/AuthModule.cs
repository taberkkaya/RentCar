﻿using RentCarServer.Application.Auth;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebApi.Modules;

public static class AuthModule
{
    public static void MapAuth(this IEndpointRouteBuilder builder)
    {
        var app = builder.MapGroup("/auth")
            .RequireRateLimiting("login-fixed");

        app.MapPost("/login",
            async (LoginCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful
                    ? Results.Ok(res)
                    : Results.InternalServerError(res);
            })
            .Produces<Result<string>>();
    }
}
