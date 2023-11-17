using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.DataManager;
using TP01_Révisions.Models.DTO;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TP01DbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TP01DBContext")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .WithOrigins("http://localhost:45184")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });




            builder.Services.AddScoped<IDataRepository<Produit>, ProduitManager>();
            builder.Services.AddScoped<IDataRepository<TypeProduit>, TypeProduitManager>();
            builder.Services.AddScoped<IDataRepository<Marque>, MarqueManager>();
            
            builder.Services.AddScoped<IDataRepositoryDTO<ProduitDto>, ProduitManager>();
            builder.Services.AddScoped<IDataRepositoryDTO<TypeProduitDto>, TypeProduitManager>();
            builder.Services.AddScoped<IDataRepositoryDTO<MarqueDto>, MarqueManager>();
            
            builder.Services.AddScoped<IDataRepositoryDetailDTO<ProduitDetailDto>, ProduitManager>();
            builder.Services.AddScoped<IDataRepositoryDetailDTO<TypeProduit>, TypeProduitManager>();
            builder.Services.AddScoped<IDataRepositoryDetailDTO<Marque>, MarqueManager>();


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
        }
    }
}