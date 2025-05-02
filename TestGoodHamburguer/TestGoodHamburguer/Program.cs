using TestGoodHamburguer.Data;
using Microsoft.EntityFrameworkCore;
using TestGoodHamburguer.Repositories.Interfaces;
using TestGoodHamburguer.Repositories;
using TestGoodHamburguer.Services.Interface;
using TestGoodHamburguer.Services;
using TestGoodHamburguer.Mappings;
using Swashbuckle.AspNetCore.Filters;
using TestGoodHamburguer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GoodHamburguerDatabase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TestGoodHamburguer", Version = "v1" });
    c.ExampleFilters(); // ✅ Agora isso funciona
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(typeof(PedidoProfile).Assembly);
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreatePedidoDtoExample>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(context);
}
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
