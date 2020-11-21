using System;
using AutoMapper;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.Extensions.DependencyInjection;
using Zedouto.Api.Login.Model.Configurations;
using Zedouto.Api.Login.Model.Entities;
using Zedouto.Api.Login.Model;
using Zedouto.Api.Login.Repository;
using Zedouto.Api.Login.Repository.Interfaces;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, FirestoreAppSettings firestoreSettings)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IJwtService<User, UserToken>, UserJwtService>();
            services.AddSingleton<ICryptographyService, CryptographSHA512Service>();
            
            services.AddSingleton<IFirestoreRepository, FirestoreRepository>();

            var builder = new FirestoreClientBuilder();
            builder.JsonCredentials = firestoreSettings.Credentials;

            // Add Firestore singleton
            services.AddSingleton(FirestoreDb.Create(firestoreSettings.ProjectId, builder.Build()).Collection(firestoreSettings.CollectionName));

            services.AddSingleton(AddMappers());
            
            return services;
        }

        private static IMapper AddMappers()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserEntity>();
                config.CreateMap<UserEntity, User>();
                config.CreateMap<Doctor, DoctorEntity>();
                config.CreateMap<DoctorEntity, Doctor>();
            })
            .CreateMapper();
        }
    }
}