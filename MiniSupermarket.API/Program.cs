using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; // Thêm dòng này để nhận diện cấu hình ổ khóa
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình dịch vụ xác thực JWT Bearer
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// 2. Thêm Controllers và dịch vụ Swagger Gen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Thêm cấu hình Bearer và nút Authorize ổ khóa tại đây
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token JWT vào đây:"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 3. Cấu hình sử dụng Swagger ở môi trường phát triển (hiển thị tại root https://localhost:7086/)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniSupermarket API V1");
        c.RoutePrefix = string.Empty; // Mở Swagger ngay tại trang chủ
    });
}

app.UseHttpsRedirection();

// 4. BẮT BUỘC: Gọi UseAuthentication TRƯỚC UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();