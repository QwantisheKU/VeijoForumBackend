using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
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

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
