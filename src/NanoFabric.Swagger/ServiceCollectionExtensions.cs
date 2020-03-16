using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NanoFabric.Core;
using System;
using System.Collections.Generic;

namespace NanoFabric.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(
            this IServiceCollection services,
            IApiInfo apiInfo

        ) => services
            .AddSwaggerGen(options =>
            {

                //TODO:

                //options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc(apiInfo.Version, new OpenApiInfo
                {
                    Title = apiInfo.Title,
                    Version = apiInfo.Version,
                    Description = apiInfo.Version
                });

                if (apiInfo.AuthenticationAuthority != null)
                {
                    options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                        Type = SecuritySchemeType.ApiKey,
                        OpenIdConnectUrl = new Uri(apiInfo.AuthenticationAuthority)
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        //{
                        //new OpenApiSecurityScheme
                        //{
                        //    Reference = new OpenApiReference()
                        //    {
                        //        Id = "Bearer",
                        //        Type = ReferenceType.SecurityScheme,
                        //    }
                        //}, Array.Empty<string>()
                        //}
                    });
                }
                options.DocumentFilter<LowerCaseDocumentFilter>();
                options.OperationFilter<AuthorizeCheckOperationFilter>(apiInfo);
                //options.OperationFilter<DescriptionOperationFilter>();
            });

        public static IApplicationBuilder UseCustomSwagger(
            this IApplicationBuilder app,
            IApiInfo apiInfo
            ) => app.UseSwagger(c =>
            {
                //c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            })
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{apiInfo.Version}/swagger.json", $"{apiInfo.Title} {apiInfo.Version}");
                if (apiInfo.AuthenticationAuthority != null)
                {
                    //c.ConfigureOAuth2(
                    //    apiInfo.SwaggerAuthInfo.ClientId,
                    //    apiInfo.SwaggerAuthInfo.Secret,
                    //    apiInfo.SwaggerAuthInfo.Realm,
                    //    $"{apiInfo.Title} - ${apiInfo.Version} - Swagger UI"
                    //);
                }
            });
    }
}
