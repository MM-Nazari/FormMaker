using FormMaker.Interface;
using FormMaker.Service;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Register services (including TestService)
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IFormService, FormService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<FormMaker.Data.Context.FormMakerDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("FormMakerDbContext")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Form Maker API", Version = "v1" });
    c.DocInclusionPredicate((docName, apiDesc) =>
    {

        if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

        // Exclude controllers without group names
        if (string.IsNullOrWhiteSpace(apiDesc.GroupName)) return false;

        // Users Groups
        // Include controllers with specific group names
        var allowedGroups = new[] {"Questions", "Processes", "Forms"};
        return allowedGroups.Contains(apiDesc.GroupName);


    });
});

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
