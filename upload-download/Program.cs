using Microsoft.AspNetCore.Http.Features;
using upload_download.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1024 * 1024 * 10; // 10 MB limit
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
