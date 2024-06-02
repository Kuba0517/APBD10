using APBD10.Contexts;
using APBD10.DTOs;
using APBD10.Exceptions;
using APBD10.Services;
using APBD10.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidators>();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/accounts/{id:int}", async (int id, IAccountService service) =>
{
    try
    {
        return Results.Ok(await service.GetAccountById(id));
    }
    catch (AccountNotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
});

app.MapPost("api/products", async (CreateProductDTO createProductDto,
    IProductService service,
    IValidator<CreateProductDTO> validator) =>
{
    var validationResult = await validator.ValidateAsync(createProductDto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    return await service.CreateProduct(createProductDto) ? Results.Created() : Results.NotFound();
});

app.Run();
