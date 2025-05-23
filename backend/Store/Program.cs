using Store.Models;
using Store.BL;
using Store.Data;
using Microsoft.EntityFrameworkCore;
using Repository;
using Store.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Web",
        Version = "v1",
        Description = "Documention API",
        Contact=new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name="VN_RU",

        }
    });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Store.xml");

    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<DbContextFactory>();
builder.Services.AddTransient<DataContext>(provider => provider.GetRequiredService<DbContextFactory>().CreateDbContext());


builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<IItemOrderRepository, ItemOrderRepository>();
builder.Services.AddTransient<ItemOrderService>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<CartService>();
builder.Services.AddTransient<IItemCartRepository, ItemCartRepository>();
builder.Services.AddTransient<ItemCartService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod() 
                   .AllowAnyHeader(); 
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
