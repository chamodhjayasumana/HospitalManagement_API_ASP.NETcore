using HospitalManagement_API.DataAccess;
using HospitalManagement_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DB_Connection>();
builder.Services.AddScoped<MainService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<MedicalRecordService>();






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


