using System;
using AutoMapper;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IEntityMapper
    {
        TMap EntityToMap<TEntity, TMap>(TEntity entity);
        TEntity MapToEntity<TEntity, TMap>(TMap map);
        TEntity MapToEntity<TEntity, TMap>(TMap map, Action<object, TEntity> mapAction);
    }
}