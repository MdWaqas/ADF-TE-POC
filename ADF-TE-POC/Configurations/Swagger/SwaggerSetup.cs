using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ADF_TE_POC.Configurations.Swagger
{
    /// <summary>
    /// Class for Setting up Swagger in the Application.
    /// </summary>
    public static class SwaggerSetup
    {
        /// <summary>
        /// Adds the swagger setup.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">services</exception>
        public static void AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var apiInfo = configuration.GetSection("SwaggerInfo").Get<SwaggerInfo>();

            services.AddSwaggerGen(op =>
            {
                op.EnableAnnotations();
                op.SwaggerDoc(apiInfo.Version, new OpenApiInfo
                {
                    Version = apiInfo.Version,
                    Title = apiInfo.Title,
                    Description = apiInfo.Description,
                    Contact = new OpenApiContact
                    {
                        Name = apiInfo.Contact.Name,
                        Email = apiInfo.Contact.Email,
                        Url = apiInfo.Contact.Url
                    },
                    License = new OpenApiLicense
                    {
                        Name = apiInfo.License.Name,
                        Url = apiInfo.License.Url
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                foreach (var xmlCommentsFilePath in XmlCommentsFilePaths)
                {
                    op.IncludeXmlComments(xmlCommentsFilePath);
                }

                // Set the validation info path from Fluent Validation.
                //op.AddFluentValidationRules();

                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });

            //Support newtonSoft now no need to user system.text
            services.AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// Uses the swagger setup.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="System.ArgumentNullException">app</exception>
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopper Value");
                c.DefaultModelsExpandDepth(0);
            });
        }

        private static IEnumerable<string> XmlCommentsFilePaths
        {
            get
            {
                var commentFiles = new List<string>();
                var basePath = AppContext.BaseDirectory;
                var apiFileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                commentFiles.Add(Path.Combine(basePath, apiFileName));
                return commentFiles;
            }
        }
    }
}
