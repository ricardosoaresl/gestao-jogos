using AutoMapper;
using GestaoJogos.Application.ApplicationServices.Commands;
using GestaoJogos.Application.ApplicationServices.Commands.Contracts;
using GestaoJogos.Application.ApplicationServices.Helper;
using GestaoJogos.Application.ApplicationServices.Queries;
using GestaoJogos.Application.ApplicationServices.Queries.Contracts;
using GestaoJogos.Application.ApplicationServices.ViewModel;
using GestaoJogos.CrossCutting.Auditing;
using GestaoJogos.CrossCutting.Notification.Config;
using GestaoJogos.CrossCutting.Notification.IoC;
using GestaoJogos.CrossCutting.Notification.RabbitMQ;
using GestaoJogos.CrossCutting.Notification.Services;
using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using GestaoJogos.Domain.Principal.Repositories.Contracts;
using GestaoJogos.Domain.Principal.Services;
using GestaoJogos.Domain.Principal.Services.Contracts;
using GestaoJogos.Infrastructure.EntityFramework.Context;
using GestaoJogos.Infrastructure.EntityFramework.Persistence.Repositories;
using GestaoJogos.Infrastructure.EntityFramework.UnitOfWork;
using GestaoJogos.Presentation.Api.Base.Filters;
using GestaoJogos.SharedKernel.Infrastructure.DataContext;
using GestaoJogos.SharedKernel.Infrastructure.UnitOfWork;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoJogos.Presentation.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapping()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Notification
            services.Configure<MessageBusOptions>(Configuration.GetSection("MessageBrokerServer"));
            services.Configure<EmailQueueConfig>(Configuration.GetSection("EmailMessage"));
            services.Configure<EmailServerConfig>(Configuration.GetSection("EmailServerConfig"));
            services.Configure<AuditManagerOptions>(Configuration.GetSection("Audit"));

            services.AddScoped<IAuditManager, AuditManager>();

            services.AddDbContext<GestaoJogosContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("GestaoJogosConnection")
                )
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestão Jogos - API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            })
                .AddControllers(options =>
                {
                    options.EnableEndpointRouting = true;
                    options.Filters.Add(new ApiExceptionFilter());

                    foreach (var formatter in options.OutputFormatters
                        .OfType<ODataOutputFormatter>()
                        .Where(it => !it.SupportedMediaTypes.Any())
                    )
                    {
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.mock-odata"));
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
                    }

                    foreach (var formatter in options.InputFormatters
                        .OfType<ODataInputFormatter>()
                        .Where(it => !it.SupportedMediaTypes.Any())
                    )
                    {
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.mock-odata"));
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                        formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
                    }
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter { AllowIntegerValues = true });
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<GestaoJogosContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            // configura auditoria
            app.UseAuditManager();

            app.UseRouting();
            app.UseCors("AllowedPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.SetTimeZoneInfo(TimeZoneInfo.Utc);
                endpoints.MapControllers();
                endpoints.Count().Select().Filter().OrderBy().MaxTop(10);
                endpoints.EnableDependencyInjection();
                endpoints.MapODataRoute("odata", "api/v1", GetEdmModel());
            });

            app.UseSwagger();
            app.UseSwaggerUI(option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão Jogos - API V1"));
            app.UseWelcomePage("/swagger");

            app.Run(context =>
            {
                context.Response.Redirect("/swagger/index.html");
                return Task.CompletedTask;
            });
        }

        private IEdmModel GetEdmModel()
        {
            var edmBuilder = new ODataConventionModelBuilder();

            edmBuilder.EntitySet<AmigoViewModel>("Amigo");

            edmBuilder.EntitySet<JogoViewModel>("Jogo")
                .EntityType.Expand(SelectExpandType.Automatic, "Amigo");

            return edmBuilder.GetEdmModel();
        }


        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDataContext>(provider => provider.GetService<GestaoJogosContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            NotificationInjectorBootStrapper.RegisterServices(services);

            //--
            services.AddScoped<IAmigoService, AmigoService>();
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IAmigoCommandServiceApplication, AmigoCommandServiceApplication>();
            services.AddScoped<IAmigoQueryServiceApplication, AmigoQueryServiceApplication>();
            //--
            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IJogoCommandServiceApplication, JogoCommandServiceApplication>();
            services.AddScoped<IJogoQueryServiceApplication, JogoQueryServiceApplication>();

            ServiceProviderAdapter.Init(services.BuildServiceProvider().GetService<IServiceProvider>());
        }

    }
}