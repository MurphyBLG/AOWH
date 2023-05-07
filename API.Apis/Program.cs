using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

    var mapperConfig = new MapperConfiguration(mc =>
    {   
        mc.AddProfile(new AccountingMap());
        mc.AddProfile(new EmployeeMap());
        mc.AddProfile(new ShiftMap());
        mc.AddProfile(new WorkPlanMap());
    });

    IMapper mapper = mapperConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();
    app.Run();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

