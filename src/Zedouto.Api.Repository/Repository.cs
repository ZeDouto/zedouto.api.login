using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Zedouto.Api.Repository.Interfaces;
using static Google.Apis.Auth.OAuth2.ComputeCredential;

namespace Zedouto.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CollectionReference _collection;

        public Repository(CollectionReference collection)
        {
            _collection = collection;
        }

        public async Task<Timestamp> AddAsync(T entity)
        {            
            var document = _collection.Document();

            var model = await document.SetAsync(entity);

            return model.UpdateTime;
        }

        public Task<T> EditAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
