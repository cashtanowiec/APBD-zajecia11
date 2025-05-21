using APBD_zajecia11.DAL;
using APBD_zajecia11.Services.Patient;
using APBD_zajecia11.Services.Prescription;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPatientService, PatientService>();


var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
