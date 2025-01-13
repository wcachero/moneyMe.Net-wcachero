using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Repositories;
using WebApplication2.Services;
using WebApplication2.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebApplication2.Model;
using LoanApplication.Interface;
using LoanApplication.Repository;
using LoanApplication.Services;
using LoanApplication.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://wcachero.github.io/MoneyMe.Angular-wcachero")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

// Add services to the container

builder.Services.AddControllers();
builder.Services.AddDbContext<MoneyMeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<BlacklistedPhoneNumbersDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BlackListedEmailDomainValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerDetailDtoValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IBlackListedEmailDomainRepository, BlackListedEmailDomainRepository>();
builder.Services.AddScoped<IBlackListedEmaiDomainService, BlackListedEmailDomainService>();
builder.Services.AddScoped<IBlacklistedPhoneNumbersRepository, BlacklistedPhoneNumbersRepository>();
builder.Services.AddScoped<IBlacklistedPhoneNumbersService, BlacklistedPhoneNumbersService>();

builder.Services.AddScoped<ICustomerDetailRepository, CustomerDetailRepository>();
builder.Services.AddScoped<ILoanProductRepository, LoanProductRepository>();
builder.Services.AddScoped<ILoanProductService, LoanProductService>();
builder.Services.AddScoped<ICustomerDetailService, CustomerDetailService>();
builder.Services.AddScoped<ICustomerLoanDetailRepository, CustomerLoanDetailRepository>();




var app = builder.Build();
// Use the CORS policy
app.UseCors("AllowLocalhost"); // Apply CORS policy globally

// Map controller endpoints
app.MapControllers();
// Add services to the container.

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MoneyMeDbContext>();
    dbContext.Database.Migrate(); // Applies any pending migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
