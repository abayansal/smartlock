using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SmartLock.Business.Handlers;
using SmartLock.Business.Projections;
using SmartLock.Persistence.Contracts;
using SmartLock.Persistence.Entities;
using SmartLock.Persistence.MongoRepositories;

namespace SmartLock.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // lets add jwt based authentication scheme to make our API only respond to desired clients
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
               
            services.AddMediatR(typeof(AuthenticateAPIClientCommandHandler).Assembly);

            AddComponents(services, appSettings);
            CreateMongoMapping();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }

        private void AddComponents(IServiceCollection services, AppSettings settings)
        {
            services.AddSingleton(settings);

            //Persistence
            services.AddSingleton(new MongoClient(new MongoUrl(settings.MongoHost)));
            services.AddTransient<IApiUserRepository, ApiUserMongoRepository>();
            services.AddTransient<IUserRepository, UserMongoRepository>();
            services.AddTransient<IGateRepository, GateMongoRepository>();
            services.AddTransient<IGrantedAccessHistoryRepository, GrantedAccessHistoryMongoRepository>();
            services.AddTransient<IUnlockGateAttemptHistoryRepository, UnlockGateAttemptHistoryMongoRepository>();

            //Command Handlers
            services.AddTransient<AuthenticateAPIClientCommandHandler>();
            services.AddTransient<CreateUserCommandHandler>();
            services.AddTransient<CreateGateCommandHandler>();
            services.AddTransient<GrantAccessCommandHandler>();
            services.AddTransient<UnlockGateCommandHandler>();

            //Projections to write events
            services.AddTransient<GrantedAccessHistoryProjection>();
            services.AddTransient<SuccessfulAttemptHistoryProjection>();
            services.AddTransient<UnsuccessfulAttemptHistoryProjection>();
        }

        private void CreateMongoMapping()
        {
            BsonClassMap.RegisterClassMap<ApiUserEntity>(c =>
            {
                c.AutoMap();
                c.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<UserEntity>(c =>
            {
                c.AutoMap();
                c.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<GateEntity>(c =>
            {
                c.AutoMap();
                c.SetIgnoreExtraElements(true);
            });
        }
    }
}
