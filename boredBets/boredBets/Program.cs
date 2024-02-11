using boredBets.Models;
using boredBets.Repositories;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace boredBets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BoredbetsContext>();
            builder.Services.AddScoped<IHorseInterface, HorseService>();
            builder.Services.AddScoped<IJockeyInterface, JockeyService>();
            builder.Services.AddScoped<ITrackInterface, TrackService>();
            builder.Services.AddScoped<IUserInterface, UserService>();
            builder.Services.AddScoped<IUserDetailInterface,UserDetailService>();
            builder.Services.AddScoped<IUserCardInterface, UserCardService>();
            builder.Services.AddScoped<IUserBetInterface,UserBetService>();
            builder.Services.AddScoped<IRaceInterface,RaceService>();
            builder.Services.AddScoped<IParticipantInterface, ParticipantService>();

            builder.Services.AddControllers();

            

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                                  });
            });
            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();

            app.UseCors(MyAllowSpecificOrigins);

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
