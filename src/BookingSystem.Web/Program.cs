// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using BookingSystem.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDocumentation();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAutoMapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
