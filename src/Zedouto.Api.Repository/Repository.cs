using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<T> GetAsync(Dictionary<string, object> filters)
        {
            var index = 0;
            Query query = null;
            
            do
            {
                var filter = filters.ElementAtOrDefault(index);

                if (!string.IsNullOrEmpty(filter.Key))
                {
                    query = _collection.WhereEqualTo(filter.Key, filter.Value);
                }

                index++;
            }
            while (index < filters.Count);

            var snapshot = await query.GetSnapshotAsync();

            return snapshot.FirstOrDefault()?.ConvertTo<T>();
        }

        public async Task<T> GetByPathIdAsync(string pathId)
        {
            var snapshot = await _collection.Document(pathId).GetSnapshotAsync();
            
            return snapshot.ConvertTo<T>();
        }

        public async Task<IEnumerable<T>> ListAsync(Dictionary<string, object> filters)
        {
            var index = 0;
            Query query = null;
            
            do
            {
                var filter = filters.ElementAtOrDefault(index);

                if (!string.IsNullOrEmpty(filter.Key))
                {
                    query = _collection.WhereEqualTo(filter.Key, filter.Value);
                }

                index++;
            }
            while (index < filters.Count);

            var snapshot = await query.GetSnapshotAsync();

            return snapshot.Cast<T>().ToList();
        }
    }
}
