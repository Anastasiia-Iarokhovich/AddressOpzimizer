using address_optimizer.client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Register IAddressClient as singleton.
builder.Services.AddSingleton<IAddressClient, AddressClient>();

// Register AutoMapper.
builder.Services.AddAutoMapper(typeof(Program));

// Add HTTP-client with the name "HttpAddressClient" and gave base address.
builder.Services.AddHttpClient("HttpAddressClient", client =>
{
    client.BaseAddress = new Uri("https://my-json-server.typicode.com/Anastasiia-Iarokhovich/json/");
});

// Add CORS.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();