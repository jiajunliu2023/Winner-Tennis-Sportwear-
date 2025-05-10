using Microsoft.EntityFrameworkCore;
using A2.Data;
using A2.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<A2_DBContext>(options => options.UseSqlite(builder.Configuration["A2Connection"]));
builder.Services.AddScoped<IA2Repo_Product, A2Repo_Product>();
builder.Services.AddScoped<IA2Repo_User, A2Repo_User>();
builder.Services.AddScoped<IA2Repo_Order, A2Repo_Order>();
builder.Services.AddScoped<IA2Repo_Refund, A2Repo_Refund>();
builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("MyAuthentication", null);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireClaim("customer"));
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
// }
// The newly added 
// app.UseCors("AllowAll");
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});


app.UseAuthorization();

app.MapControllers();


app.Run();



