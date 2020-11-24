namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IJwtService<TModel, TToken> where TModel : class where TToken : class
    {
        TToken SerializeToken(TModel model);
        TModel DeserializeToken(TToken token);
    }
}