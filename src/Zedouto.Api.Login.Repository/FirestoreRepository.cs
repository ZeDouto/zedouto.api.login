using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Zedouto.Api.Login.Repository.Interfaces;
using static Google.Apis.Auth.OAuth2.ComputeCredential;

namespace Zedouto.Api.Login.Repository
{
    public class FirestoreRepository : IFirestoreRepository
    {
        private readonly CollectionReference _collection;

        private const string NESTED_PROPERTY_TEMPLATE_NAME = "property";

        public FirestoreRepository(CollectionReference collection)
        {
            _collection = collection;
        }

        public async Task<string> AddAsync<T>(T entity)
        {
            var document = _collection.Document();

            await document.SetAsync(entity);
            
            return document.Id;
        }

        public async Task<string> AddNestedAsync<T>(T entity, string nestedPropName, string firestoreDocumentId)
        {
            if(string.IsNullOrWhiteSpace(firestoreDocumentId))
            {
                return default(string);
            }
            
            var document = _collection.Document(firestoreDocumentId);

            nestedPropName = string.IsNullOrWhiteSpace(nestedPropName)
                            ? NESTED_PROPERTY_TEMPLATE_NAME
                            : nestedPropName;

            var model = await document.UpdateAsync(new Dictionary<string, object> { {nestedPropName, entity} });

            return document.Id;
        }

        public async Task<T> GetAsync<T>(Dictionary<string, object> filters, params string[] fieldsReturn)
        {
            var filter = filters.First();
            var query = _collection.WhereEqualTo(filter.Key, filter.Value);

            for (var i = 1; i < filters.Count; i++)
            {
                filter = filters.ElementAt(i);

                query = query.WhereEqualTo(filter.Key, filter.Value);
            }

            var snapshot = await query.GetSnapshotAsync();

            if(snapshot.Count > 0)
            {
                var documentSnapshot = snapshot.FirstOrDefault();

                return documentSnapshot.Exists
                    ? documentSnapshot.ConvertTo<T>()
                    : default;
            }
            
            return default;
        }

        public async Task<T> GetByDocumentId<T>(string documentId)
        {
            var snapshot = await _collection.Document(documentId).GetSnapshotAsync();
            
            return snapshot.ConvertTo<T>();
        }

        public async Task<IEnumerable<T>> ListAsync<T>(IEnumerable<Dictionary<string, object>> filters, params string[] fieldsReturn)
        {
            var entities = new List<T>();

            var queries = filters.Select(filter =>
            {
                var query = _collection.WhereEqualTo(filter.First().Key, filter.First().Value);
                
                return filter.Aggregate(query, (query, filter) => query.WhereEqualTo(filter.Key, filter.Value));
            });
            
            foreach(var query in queries)
            {
                var snapshot = await query.GetSnapshotAsync();

                entities.AddRange(snapshot.Select(s => s.ConvertTo<T>()));
            }

            return entities;
        }

        public async Task<IEnumerable<T>> ContainsAsync<T>(Dictionary<string, IEnumerable<object>> elements, params string[] fieldsReturn)
        {            
            var query = _collection.Select(fieldsReturn);

            query = elements.Aggregate(query, (quey, element) => query.WhereIn(element.Key, element.Value));

            var snapshot = await query.GetSnapshotAsync();

            return snapshot.Select(s => s.ConvertTo<T>());
        }

        public async Task<IEnumerable<T>> ListAsync<T>(params string[] fieldsReturn)
        {
            var query = _collection.Select(fieldsReturn);

            var snapshot = await query.GetSnapshotAsync();

            return snapshot.Select(s => s.ConvertTo<T>());
        }
    }
}
