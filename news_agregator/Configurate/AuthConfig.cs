﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Configurate
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration, JwtOptions jwtOptions)
        {
            if (string.IsNullOrEmpty(jwtOptions.SecretKey) || jwtOptions.SecretKey.Length < 32)
            {
                throw new ArgumentException("JWT SecretKey must be at least 32 characters long");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookie"];
                            return Task.CompletedTask;
                        }
                    };
                });
            
            services.AddAuthorization();
        }
    }
}