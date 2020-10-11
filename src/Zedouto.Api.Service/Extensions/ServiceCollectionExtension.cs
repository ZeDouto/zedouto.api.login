using System;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Zedouto.Api.Model.Configurations;
using Zedouto.Api.Repository;
using Zedouto.Api.Repository.Interfaces;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Service.Extensions
{
    public static class ServiceCollectionExtension
    {                
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, FirestoreAppSettings firestoreSettings)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            // Add Firestore singleton
            services.AddSingleton(FirestoreDb.Create(firestoreSettings.ProjectId).Collection(firestoreSettings.CollectionName));
            
            return services;
        }
    }
}