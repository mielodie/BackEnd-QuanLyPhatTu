using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Implements;
using QuanLyPhatTu_API.Service.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Auth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Làm theo mẫu này. Example: Bearer {Token} ",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDaoTrangService, DaoTrangService>();
builder.Services.AddScoped<IBaiVietService, BaiVietService>();
builder.Services.AddScoped<IChuaService, ChuaService>();
builder.Services.AddScoped<IDonDangKyService, DonDangKyService>();
builder.Services.AddScoped<IPhatTuService, PhatTuService>();
builder.Services.AddScoped<ILoaiBaiVietService, LoaiBaiVietService>();
builder.Services.AddScoped<INguoiDungThichBaiVietService, NguoiDungThichBaiVietService>();
builder.Services.AddScoped<IBinhLuanBaiVietService, BinhLuanBaiVietService>();
builder.Services.AddScoped<INguoiDungThichBinhLuanBaiVietService, NguoiDungThichBinhLuanBaiVietService>();
builder.Services.AddSingleton<ResponseObject<BinhLuanBaiVietDTO>>();
builder.Services.AddSingleton<ResponseObject<NguoiDungThichBinhLuanBaiVietDTO>>();
builder.Services.AddSingleton<ResponseObject<LoaiBaiVietDTO>>();
builder.Services.AddSingleton<ResponseObject<PhatTuDTO>>();
builder.Services.AddSingleton<ResponseObject<TokenDTO>>();
builder.Services.AddSingleton<ResponseObject<PhatTuDTO>>();
builder.Services.AddSingleton<ResponseObject<DonDangKyDTO>>();
builder.Services.AddSingleton<ResponseObject<ChuaDTO>>();
builder.Services.AddSingleton<PhatTuConverter>();
builder.Services.AddSingleton<DaoTrangConverter>();
builder.Services.AddSingleton<ChuaConverter>();
builder.Services.AddSingleton<DonDangKyConverter>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<NguoiDungThichBinhLuanBaiVietConverter>();
builder.Services.AddControllers().AddJsonOptions(options =>

{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:SecretKey").Value!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
