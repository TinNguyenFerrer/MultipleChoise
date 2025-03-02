using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.Repositorys;
using MultipleChoise.Server.Data.UnitOfWork;
using MultipleChoise.Server.Service.Interface;
using MultipleChoise.Server.Service;
using Microsoft.AspNetCore.Hosting;
using MultipleChoise.Server.Contract.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MultipleChoiseDbContext>(
    options => options.UseSqlite("Data Source=MultipleChoiseDB.db")
    );

// Repo layer
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Service layer
builder.Services.AddScoped<ICrudService<AnswerDto>, AnswerService>();
builder.Services.AddScoped<ICrudService<QuestionDto>, QuestionService>();
builder.Services.AddScoped<ICrudService<QuizDto>, QuizService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IResultService, ResultService>();

builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Use Cors
app.UseCors("AllowSpecificOrigin");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapDefaultControllerRoute();

app.MapFallbackToFile("/index.html");

app.Run();
