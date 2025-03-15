// <copyright file="WebApplicationExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookingSystem.Web.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplication"/>.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds documentation services.
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/> instance.</param>
    /// <returns><paramref name="app"/>.</returns>
    public static WebApplication UseDocumentation(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        return app;
    }
}
