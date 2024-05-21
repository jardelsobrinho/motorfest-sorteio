using ServerSorterio.Api.Configs;
using ServerSorterio.Api.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder.AllowAnyMethod().
            AllowAnyHeader().
            AllowCredentials().
            SetIsOriginAllowed((hosts) => true));
});

ConexaoHelper.LocalDatabase = builder.Configuration.GetConnectionString("DefaultConnection");
ConexaoHelper.CriarBancoSQLite();

var app = builder.Build();
app.UseErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
