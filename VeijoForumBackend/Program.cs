using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Repositories;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services;
using VeijoForumBackend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VeijoForumBackendContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MisisForumContext") ?? throw new InvalidOperationException("Connection string 'MisisForumContext' not found.")));

// Register Repositories
builder.Services.AddTransient<ITopicRepository, TopicRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

// Register Services
builder.Services.AddTransient<ITopicService, TopicService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services
    .AddIdentity<User, Role>(opt =>
    {
        opt.Password = new PasswordOptions
        {
            RequireDigit = false,
            RequiredLength = 6,
            RequireNonAlphanumeric = false,
            RequireUppercase = false
        };
        opt.User.RequireUniqueEmail = true;
    })
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<VeijoForumBackendContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
