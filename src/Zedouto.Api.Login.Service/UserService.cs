using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Zedouto.Api.Login.Model.Entities;
using Zedouto.Api.Login.Model;
using Zedouto.Api.Login.Repository.Interfaces;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Service
{
    public class UserService : IUserService, IEntityMapper
    {
        private readonly IFirestoreRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICryptographyService _cryptographyService;
        
        public UserService(IFirestoreRepository repository, IMapper mapper, ICryptographyService cryptographyService)
        {
            _repository = repository;
            _mapper = mapper;
            _cryptographyService = cryptographyService;
        }
        
        public async Task AddUserAsync(User user)
        {
            var userEntity = MapToEntity<UserEntity, User>(user, (obj, entity) =>
            {
                entity.Password = string.IsNullOrWhiteSpace(entity.Password) ? null : _cryptographyService.Cryptograph(entity.Password);
                entity.Doctor = null;
            });
            
            var documentId = await _repository.AddAsync(userEntity);

            if(user.Doctor != default(Doctor))
            {
                var doctorEntity = MapToEntity<DoctorEntity, Doctor>(user.Doctor);

                await _repository.AddNestedAsync(doctorEntity, documentId, nameof(Doctor));
            }
        }

        public async Task<User> GetUserAsync(User user)
        {
            var entity = await _repository.GetAsync<UserEntity>(new Dictionary<string, object> {
                { nameof(User.Cpf), user.Cpf }
            });

            return EntityToMap<UserEntity, User>(entity);
        }

        public async Task<User> GetByLoginAndSenhaAsync(User user)
        {
            var entity = await _repository.GetAsync<UserEntity>(new Dictionary<string, object> {
                { nameof(User.Login), user.Login },
                { nameof(User.Password), _cryptographyService.Cryptograph(user.Password) }
            });
            
            return EntityToMap<UserEntity, User>(entity);
        }

        public TMap EntityToMap<TEntity, TMap>(TEntity entity)
        {
            return _mapper.Map<TMap>(entity);
        }

        public TEntity MapToEntity<TEntity, TMap>(TMap map)
        {            
            return _mapper.Map<TEntity>(map);
        }

        public TEntity MapToEntity<TEntity, TMap>(TMap map, Action<object, TEntity> mapAction)
        {
            return _mapper.Map<TEntity>(map, options =>
            {
                options.AfterMap(mapAction);
            });
        }
    }
}
